using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic3DStarter
{
    public class Triangle
    {

        VertexPositionColor[] vertices;

        BasicEffect effect;

        Game game;

        public Triangle(Game game)
        {
            this.game = game;
            InitializeVertices();
            InitializeEffect();
        }

        void InitializeVertices()
        {
            vertices = new VertexPositionColor[3];
            // vertex 0
            vertices[0].Position = new Vector3(0, 1, 0);
            vertices[0].Color = Color.Red;
            // vertex 1
            vertices[1].Position = new Vector3(1, 1, 0);
            vertices[1].Color = Color.Green;
            // vertex 2 
            vertices[2].Position = new Vector3(1, 0, 0);
            vertices[2].Color = Color.Blue;
        }

        /// <summary>
        /// Initializes the BasicEffect to render our triangle
        /// </summary>
        void InitializeEffect()
        {
            effect = new BasicEffect(game.GraphicsDevice);
            effect.World = Matrix.Identity;
            effect.View = Matrix.CreateLookAt(
                new Vector3(0, 0, 4), // The camera position
                new Vector3(0, 0, 0), // The camera target,
                Vector3.Up            // The camera up vector
            );
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,                         // The field-of-view 
                game.GraphicsDevice.Viewport.AspectRatio,   // The aspect ratio
                0.1f, // The near plane distance 
                100.0f // The far plane distance
            );
            effect.VertexColorEnabled = true;
        }

        public void Update(GameTime gameTime)
        {
            float angle = (float)gameTime.TotalGameTime.TotalSeconds;
            effect.World = Matrix.CreateRotationY(angle);
        }


        public void Draw()
        {
            // Cache old rasterizer state
            RasterizerState oldState = game.GraphicsDevice.RasterizerState;

            // Disable backface culling 
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            game.GraphicsDevice.RasterizerState = rasterizerState;

            // Apply our effect
            effect.CurrentTechnique.Passes[0].Apply();

            // Draw the triangle
            game.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(
                PrimitiveType.TriangleList,
                vertices,       // The vertex data 
                0,              // The first vertex to use
                1               // The number of triangles to draw
            );

            // Restore the prior rasterizer state 
            game.GraphicsDevice.RasterizerState = oldState;
        }


    }
}
