﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwistedLogik.Ultraviolet;

namespace WindowsFormsApplication1
{
    public class TestObject
    {
        public TestObject()
        {
            SomeMatrix = Matrix.Identity;
        }

        public Color SomeColor { get; set; }
        public Vector2 SomeVector2 { get; set; }
        public Vector3 SomeVector3 { get; set; }
        public Vector4 SomeVector4 { get; set; }
        public Matrix SomeMatrix { get; set; }
        public Size2 SomeSize2 { get; set; }
        public Size2F SomeSize2F { get; set; }
        public Size3 SomeSize3 { get; set; }
        public Size3F SomeSize3F { get; set; }
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = new TestObject();
        }
    }
}
