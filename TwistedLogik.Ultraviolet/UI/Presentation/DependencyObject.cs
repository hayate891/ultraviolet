﻿using System;
using System.Collections.Generic;
using TwistedLogik.Nucleus;
using TwistedLogik.Ultraviolet.UI.Presentation.Animations;

namespace TwistedLogik.Ultraviolet.UI.Presentation
{
    /// <summary>
    /// Represents an object that participates in the dependency property system.
    /// </summary>
    public abstract partial class DependencyObject
    {
        /// <summary>
        /// Evaluates whether any of the object's dependency property values have changed and, if so, invokes the appropriate callbacks.
        /// </summary>
        /// <param name="time">Time elapsed since the last call to <see cref="UltravioletContext.Update(UltravioletTime)"/>.</param>
        public void Digest(UltravioletTime time)
        {
            for (int i = 0; i < digestedDependencyProperties.Count; i++)
            {
                digestedDependencyProperties[i].Digest(time);
            }
        }

        /// <summary>
        /// Clears the local values on all of the object's dependency properties.
        /// </summary>
        public void ClearLocalValues()
        {
            foreach (var kvp in dependencyPropertyValues)
            {
                kvp.Value.ClearLocalValue();
            }
        }

        /// <summary>
        /// Clears the styled values of all of the object's dependency properties.
        /// </summary>
        public void ClearStyledValues()
        {
            foreach (var kvp in dependencyPropertyValues)
            {
                kvp.Value.ClearStyledValue();
            }
        }

        /// <summary>
        /// Applies the specified animation to a dependency property.
        /// </summary>
        /// <param name="dp">A <see cref="DependencyProperty"/> instance which identifies the dependency property to animate.</param>
        /// <param name="animation">The animation to apply to the dependency property, or <c>null</c> to cease animating the property.</param>
        /// <param name="clock">The clock which controls the animation's playback.</param>
        public void Animate(DependencyProperty dp, AnimationBase animation, StoryboardClock clock)
        {
            var wrapper = GetDependencyPropertyValue(dp, dp.PropertyType);
            wrapper.Animate(animation, clock);
        }

        /// <summary>
        /// Binds a dependency property to a property on a model.
        /// </summary>
        /// <param name="dp">A <see cref="DependencyProperty"/> instance which identifies the dependency property to bind.</param>
        /// <param name="viewModelType">The type of view model to which to bind the property.</param>
        /// <param name="expression">The binding expression with which to bind the property.</param>
        public void BindValue(DependencyProperty dp, Type viewModelType, String expression)
        {
            Contract.Require(dp, "dp");

            var wrapper = GetDependencyPropertyValue(dp, dp.PropertyType);
            wrapper.Bind(viewModelType, expression);
        }

        /// <summary>
        /// Unbinds a dependency property.
        /// </summary>
        /// <param name="dp">A <see cref="DependencyProperty"/> instance which identifies the dependency property to unbind.</param>
        public void UnbindValue(DependencyProperty dp)
        {
            Contract.Require(dp, "dp");

            var wrapper = GetDependencyPropertyValue(dp, dp.PropertyType);
            wrapper.Unbind();
        }

        /// <summary>
        /// Gets the value typed value of the specified dependency property.
        /// </summary>
        /// <typeparam name="T">The type of value contained by the dependency property.</typeparam>
        /// <param name="dp">A <see cref="DependencyProperty"/> instance which identifies the dependency property for which to set a value.</param>
        /// <returns>The value of the specified dependency property.</returns>
        public T GetValue<T>(DependencyProperty dp)
        {
            Contract.Require(dp, "dp");

            if (!typeof(T).Equals(dp.PropertyType))
                throw new InvalidCastException();

            var wrapper = GetDependencyPropertyValue<T>(dp);
            return wrapper.GetValue();
        }

        /// <summary>
        /// Sets the value of the specified dependency property.
        /// </summary>
        /// <typeparam name="T">The type of value contained by the dependency property.</typeparam>
        /// <param name="dp">A <see cref="DependencyProperty"/> instance which identifies the dependency property for which to set a value.</param>
        /// <param name="value">The value to set on the specified dependency property.</param>
        public void SetValue<T>(DependencyProperty dp, T value)
        {
            Contract.Require(dp, "dp");

            if (!typeof(T).Equals(dp.PropertyType))
                throw new InvalidCastException();

            var wrapper = GetDependencyPropertyValue<T>(dp);
            wrapper.SetValue(value);
        }

        /// <summary>
        /// Sets the default value of the specified dependency property.
        /// </summary>
        /// <typeparam name="T">The type of value contained by the dependency property.</typeparam>
        /// <param name="dp">A <see cref="DependencyProperty"/> instance which identifies the dependency property for which to set a value.</param>
        /// <param name="value">The value to set on the specified dependency property.</param>
        public void SetDefaultValue<T>(DependencyProperty dp, T value)
        {
            Contract.Require(dp, "dp");

            if (!typeof(T).Equals(dp.PropertyType))
                throw new InvalidCastException();

            var wrapper = GetDependencyPropertyValue<T>(dp);
            wrapper.DefaultValue = value;
        }

        /// <summary>
        /// Sets the local value of the specified dependency property.
        /// </summary>
        /// <typeparam name="T">The type of value contained by the dependency property.</typeparam>
        /// <param name="dp">A <see cref="DependencyProperty"/> instance which identifies the dependency property for which to set a value.</param>
        /// <param name="value">The value to set on the specified dependency property.</param>
        public void SetLocalValue<T>(DependencyProperty dp, T value)
        {
            Contract.Require(dp, "dp");

            if (!typeof(T).Equals(dp.PropertyType))
                throw new InvalidCastException();

            var wrapper = GetDependencyPropertyValue<T>(dp);
            wrapper.LocalValue = value;
        }

        /// <summary>
        /// Sets the styled value of the specified dependency property.
        /// </summary>
        /// <typeparam name="T">The type of value contained by the dependency property.</typeparam>
        /// <param name="dp">A <see cref="DependencyProperty"/> instance which identifies the dependency property for which to set a value.</param>
        /// <param name="value">The value to set on the specified dependency property.</param>
        public void SetStyledValue<T>(DependencyProperty dp, T value)
        {
            Contract.Require(dp, "dp");

            if (!typeof(T).Equals(dp.PropertyType))
                throw new InvalidCastException();

            var wrapper = GetDependencyPropertyValue<T>(dp);
            wrapper.StyledValue = value;
        }

        /// <summary>
        /// Removes the specified dependency property's current animation, if it has one.
        /// </summary>
        /// <typeparam name="T">The type of value contained by the dependency property.</typeparam>
        /// <param name="dp">A <see cref="DependencyProperty"/> instance which identifies the dependency property to clear.</param>
        public void ClearAnimation<T>(DependencyProperty dp)
        {
            Contract.Require(dp, "dp");

            var wrapper = GetDependencyPropertyValue<T>(dp);
            wrapper.ClearAnimation();
        }

        /// <summary>
        /// Clears the local value associated with the specified dependency property.
        /// </summary>
        /// <typeparam name="T">The type of value contained by the dependency property.</typeparam>
        /// <param name="dp">A <see cref="DependencyProperty"/> instance which identifies the dependency property to clear.</param>
        public void ClearLocalValue<T>(DependencyProperty dp)
        {
            Contract.Require(dp, "dp");

            var wrapper = GetDependencyPropertyValue<T>(dp);
            wrapper.ClearLocalValue();
        }

        /// <summary>
        /// Clears the styled value associated with the specified 
        /// </summary>
        /// <typeparam name="T">The type of value contained by the dependency property.</typeparam>
        /// <param name="dp">A <see cref="DependencyProperty"/> instance which identifies the dependency property to clear.</param>
        public void ClearStyledValue<T>(DependencyProperty dp)
        {
            Contract.Require(dp, "dp");

            var wrapper = GetDependencyPropertyValue<T>(dp);
            wrapper.ClearStyledValue();
        }

        /// <summary>
        /// Occurs when a dependency property which potentially affects the object's layout is changed.
        /// </summary>
        protected internal virtual void OnMeasureAffectingPropertyChanged()
        {

        }

        /// <summary>
        /// Gets the dependency object's containing object.
        /// </summary>
        protected internal abstract DependencyObject DependencyContainer
        {
            get;
        }

        /// <summary>
        /// Gets or sets the data source from which the object's dependency properties will retrieve values if they are data bound.
        /// </summary>
        protected internal abstract Object DependencyDataSource
        {
            get;
        }

        /// <summary>
        /// Gets an instance of <see cref="DependencyPropertyValue{T}"/> for the specified value typed dependency property.
        /// </summary>
        /// <typeparam name="T">The type of value contained by the dependency property.</typeparam>
        /// <param name="dp">The dependency property for which to create a value wrapper.</param>
        /// <returns>The <see cref="DependencyPropertyValue{T}"/> instance which was retrieved.</returns>
        private DependencyPropertyValue<T> GetDependencyPropertyValue<T>(DependencyProperty dp)
        {
            return (DependencyPropertyValue<T>)GetDependencyPropertyValue(dp, typeof(T));
        }

        /// <summary>
        /// Gets an instance of <see cref="IDependencyPropertyValue"/> for the specified value typed dependency property.
        /// </summary>
        /// <param name="dp">The dependency property for which to create a value wrapper.</param>
        /// <param name="type">The type of value contained by the dependency property.</param>
        /// <returns>The <see cref="IDependencyPropertyValue"/> instance which was retrieved.</returns>
        private IDependencyPropertyValue GetDependencyPropertyValue(DependencyProperty dp, Type type)
        {
            IDependencyPropertyValue valueWrapper;
            dependencyPropertyValues.TryGetValue(dp.ID, out valueWrapper);

            if (valueWrapper == null)
            {
                var dpValueType = typeof(DependencyPropertyValue<>).MakeGenericType(type);
                valueWrapper = (IDependencyPropertyValue)Activator.CreateInstance(dpValueType, this, dp);
                dependencyPropertyValues[dp.ID] = valueWrapper;
            }
            return valueWrapper;
        }

        /// <summary>
        /// Updates the specified dependency property's participation in the digest cycle.
        /// </summary>
        /// <param name="value">The dependency property to update.</param>
        /// <param name="digest">A value indicating whether the dependency property should participate in the digest cycle.</param>
        private void UpdateDigestParticipation(IDependencyPropertyValue value, Boolean digest)
        {
            if (digest)
            {
                if (!digestedDependencyProperties.Contains(value))
                {
                    digestedDependencyProperties.Add(value);
                }
            }
            else
            {
                digestedDependencyProperties.Remove(value);
            }
        }

        // The list of values for this object's dependency properties.
        private readonly Dictionary<Int64, IDependencyPropertyValue> dependencyPropertyValues =
            new Dictionary<Int64, IDependencyPropertyValue>();

        // The list of dependency properties which need to participate in the digest cycle.
        private readonly List<IDependencyPropertyValue> digestedDependencyProperties = 
            new List<IDependencyPropertyValue>();
    }
}
