﻿using System;
using TwistedLogik.Nucleus;
using TwistedLogik.Ultraviolet.Input;
using TwistedLogik.Ultraviolet.UI.Presentation.Controls.Primitives;
using TwistedLogik.Ultraviolet.UI.Presentation.Input;

namespace TwistedLogik.Ultraviolet.UI.Presentation.Controls
{
    /// <summary>
    /// Represents a control that contains multiple tabbed pages.
    /// </summary>
    [UvmlKnownType(null, "TwistedLogik.Ultraviolet.UI.Presentation.Controls.Templates.TabControl.xml")]
    [UvmlPlaceholder("ItemsPanel", typeof(TabPanel))]
    public class TabControl : Selector
    {
        /// <summary>
        /// Initializes the <see cref="TabControl"/> type.
        /// </summary>
        static TabControl()
        {
            IsTabStopProperty.OverrideMetadata(typeof(TabControl), new PropertyMetadata<Boolean>(CommonBoxedValues.Boolean.False));
            KeyboardNavigation.DirectionalNavigationProperty.OverrideMetadata(typeof(TabControl), new PropertyMetadata<KeyboardNavigationMode>(KeyboardNavigationMode.Contained));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TabControl"/> class.
        /// </summary>
        /// <param name="uv">The Ultraviolet context.</param>
        /// <param name="name">The element's identifying name within its namescope.</param>
        public TabControl(UltravioletContext uv, String name)
            : base(uv, name)
        {
            Classes.Add("top");
        }

        /// <summary>
        /// Gets or sets a <see cref="Dock"/> value which specifies how the tab headers are positioned relative to their content.
        /// </summary>
        public Dock TabStripPlacement
        {
            get { return GetValue<Dock>(TabStripPlacementProperty); }
            set { SetValue(TabStripPlacementProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="TabStripPlacement"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TabStripPlacementProperty = DependencyProperty.Register("TabStripPlacement", typeof(Dock), typeof(TabControl),
            new PropertyMetadata<Dock>(Dock.Top, HandleTabStripPlacementPropertyChanged));

        /// <summary>
        /// Called to inform the tab control that one of its items was clicked.
        /// </summary>
        /// <param name="container">The item container that was clicked.</param>
        internal void HandleItemClicked(TabItem container)
        {
            var item = ItemContainerGenerator.ItemFromContainer(container);
            if (item == null)
                return;

            var dobj = item as DependencyObject;
            if (dobj == null)
                return;

            BeginChangeSelection();

            UnselectAllItems();
            SelectItem(item);

            EndChangeSelection();
        }

        /// <summary>
        /// Called to inform the tab control that one of its items changed its content.
        /// </summary>
        /// <param name="item">The item that was changed.</param>
        internal void HandleItemContentChanged(TabItem item)
        {
            if (SelectedIndex < 0 || SelectedIndex >= Items.Count)
                return;

            if (item != Items[SelectedIndex])
                return;

            UpdateTabContent();
        }

        /// <inheritdoc/>
        protected internal override Panel CreateItemsPanel()
        {
            return new TabPanel(Ultraviolet, null);
        }

        /// <inheritdoc/>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TabItem(Ultraviolet, null);
        }

        /// <inheritdoc/>
        protected override Boolean IsItemContainer(DependencyObject element)
        {
            return element is TabItem;
        }

        /// <inheritdoc/>
        protected override Boolean IsItemContainerForItem(DependencyObject container, Object item)
        {
            var ti = container as TabItem;
            if (ti == null)
                return false;

            return ti.Content == item;
        }

        /// <inheritdoc/>
        protected override void OnSelectionChanged()
        {
            UpdateTabContent();

            base.OnSelectionChanged();
        }

        /// <inheritdoc/>
        protected override void OnItemsChanged()
        {
            if ((SelectedIndex < 0 || SelectedIndex > Items.Count) && Items.Count > 0)
            {
                SelectedIndex = 0;
            }
            base.OnItemsChanged();
        }

        /// <inheritdoc/>
        protected override void OnKeyDown(KeyboardDevice device, Key key, ModifierKeys modifiers, ref RoutedEventData data)
        {
            var target = default(UIElement);

            switch (key)
            {
                case Key.Tab:
                    if ((modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                    {
                        if ((modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                        {
                            target = GetNextEnabledTabItem(SelectedIndex, -1);
                        }
                        else
                        {
                            target = GetNextEnabledTabItem(SelectedIndex, 1);
                        }
                    }
                    break;

                case Key.Home:
                    if (Items.Count > 0)
                    { 
                        target = GetNextEnabledTabItem(Items.Count - 1, 1);
                    }
                    break;

                case Key.End:
                    if (Items.Count > 0)
                    {
                        target = GetNextEnabledTabItem(0, -1);
                    }
                    break;
            }

            if (target != null && !target.IsFocused)
            {
                data.Handled = target.Focus();
            }

            base.OnKeyDown(device, key, modifiers, ref data);
        }

        /// <summary>
        /// Called when the value of the <see cref="TabStripPlacement"/> dependency property changes.
        /// </summary>
        private static void HandleTabStripPlacementPropertyChanged(DependencyObject dobj, Dock oldValue, Dock newValue)
        {
            var tabControl = (TabControl)dobj;

            tabControl.Classes.Remove("left");
            tabControl.Classes.Remove("top");
            tabControl.Classes.Remove("right");
            tabControl.Classes.Remove("bottom");

            switch (newValue)
            {
                case Dock.Left:
                    tabControl.Classes.Add("left");
                    break;

                case Dock.Top:
                    tabControl.Classes.Add("top");
                    break;

                case Dock.Right:
                    tabControl.Classes.Add("right");
                    break;

                case Dock.Bottom:
                    tabControl.Classes.Add("bottom");
                    break;
            }
        }

        /// <summary>
        /// Updates the displayed tab content.
        /// </summary>
        private void UpdateTabContent()
        {
            if (PART_ContentPresenter != null)
            {
                var item = SelectedItem as TabItem;
                if (item != null)
                {
                    var content = item.Content;
                    PART_ContentPresenter.Content = content;
                }
                else
                {
                    PART_ContentPresenter.Content = null;
                }
            }
        }

        /// <summary>
        /// Gets the next enabled and visible tab after the specified tab.
        /// </summary>
        private TabItem GetNextEnabledTabItem(Int32 start, Int32 delta)
        {
            if (delta == 0)
                return null;

            var count = Items.Count;
            var current = (count + ((start + delta) % count)) % count;

            for (int i = 0; i < Items.Count; i++)
            {
                var container = ItemContainerGenerator.ContainerFromIndex(current) as TabItem;
                if (container != null && container.IsEnabled && container.Visibility == Visibility.Visible)
                {
                    return container;
                }

                current = (count + ((start + delta) % count)) % count;
            }

            return null;
        }

        // Component references.
        private readonly ContentPresenter PART_ContentPresenter = null;
    }
}