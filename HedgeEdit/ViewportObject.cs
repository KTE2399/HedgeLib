﻿using HedgeLib.Models;
using OpenTK;
using OpenTK.Graphics.ES30;
using System;
using System.Drawing;

namespace HedgeEdit
{
    public class ViewportObject
    {
        // Variables/Constants
        public Vector2 Position = Vector2.Zero;
        public Vector2 Size = Vector2.Zero;
        public Vector4 Crop = Vector4.Zero;
        public object CustomData = null;
        public int Texture = 0;
        
        // Constructors
        public ViewportObject()
        {
            
        }

        // Methods
        public ViewportObject(string filePath)
        {
            var bitmap = new Bitmap(filePath);
            Size = new Vector2(bitmap.Width, bitmap.Height);
            Texture = Viewport.LoadTexture(bitmap, filePath);
        }

        public void Draw()
        {
            if (Crop == Vector4.Zero)
            {
                Viewport.DrawTexturedRect(Position.X, Position.Y, Size.X, Size.Y, Texture);
            }
            else
            {
                Viewport.DrawTexturedRect(Position.X, Position.Y, Size.X, Size.Y, Crop.X, Crop.Y, Crop.Z, Crop.W, Texture);
            }
        }
    }
}