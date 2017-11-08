using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Sprites
{
    class ChasingEnemy: Enemy
    {
        // Rectangle to detect if the player is close by
        private Rectangle collisionField;

        public Point ColliionFieldscale = new Point(2,2);
        bool viewCollisionField = true;
        Texture2D collisionRectTexture;
        
        public ChasingEnemy(Game g, Texture2D texture, Vector2 Position1, int framecount) 
             : base(g,texture,Position1,framecount)
        {
            //Image for the rectangle only viewed if viewCollisionField is on
            collisionRectTexture = g.Content.Load<Texture2D>("rectFrame");

            startPosition = Position1;
            
        }

        public void follow(Player p)
        {
            // if the player passed is contained in the chase enemy collision field rectangle
            if (this.collisionField.Contains(new Point((int)p.position.X, (int)p.position.Y)))
            {
                // The direction that the player is in is calculated
                // by subtracting the position vector of this enemy sprite 
                // from the position of the player
                Vector2 direction = p.position - this.position;
                // We have to normalize the vector otherwise the jump is too big
                direction.Normalize();
                // we update the position of the Enemy Sprite based on the 
                this.position += direction * Velocity;
            }            
            
        }

        public override void Update(GameTime g)
        {
            // Calculate the collison field
            collisionField = BoundingRect;
            collisionField.Location -= collisionField.Size * ColliionFieldscale;
            collisionField.Size += (collisionField.Size + collisionField.Size) * ColliionFieldscale;
            // call the base update => try Enemy Class => try Sprite Class
            base.Update(g);
        }

        public override void Draw(SpriteBatch sp)
        {
            
                sp.Begin();
                if (viewCollisionField)
                    sp.Draw(collisionRectTexture,collisionField,Color.White);
                sp.End();
            base.Draw(sp);
        }

    }
}
