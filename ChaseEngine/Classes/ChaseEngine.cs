using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Sprites;

namespace Engines
{
    class ChaseEngine
    {
        Player p;
        
        SpriteBatch spriteBatch;
        private PlatformEnemy eplatformer;
        private ChasingEnemy[] chasers;
        private Game _gameOwnedBy;

        public ChaseEngine(Game game)
            {
                // Chase engine remembers reference to the game
                _gameOwnedBy = game;
                game.IsMouseVisible = true;
                SoundEffect[] _PlayerSounds = new SoundEffect[5];
                spriteBatch = new SpriteBatch(game.GraphicsDevice);

                for (int i = 0; i < _PlayerSounds.Length; i++)
                    _PlayerSounds[i] =
                        game.Content.Load<SoundEffect>(@"Audio/" 
                            + i.ToString());

                
                p = new Player(new Texture2D[] {game.Content.Load<Texture2D>(@"Images/left"),
                                                game.Content.Load<Texture2D>(@"Images/right"),
                                                game.Content.Load<Texture2D>(@"Images/up"),
                                                game.Content.Load<Texture2D>(@"Images/down"),
                                                game.Content.Load<Texture2D>(@"Images/stand")},
                    _PlayerSounds ,
                        new Vector2(200, 200),6,0,5.0f);

                eplatformer = new PlatformEnemy(game,
                        game.Content.Load<Texture2D>(@"Images/chaser"), new Vector2(100, 100),
                        new Vector2(300,100), 1);
            chasers = new ChasingEnemy[Utility.NextRandom(2, 5)];

            for (int i = 0; i < chasers.Count(); i++)
                {
                    chasers[i] = new ChasingEnemy(game,
                            game.Content.Load<Texture2D>(@"Images/chaser"), 
                            new Vector2(Utility.NextRandom(game.GraphicsDevice.Viewport.Width),
                                Utility.NextRandom(game.GraphicsDevice.Viewport.Height)),
                             1);
                    chasers[i].Velocity = (float)Utility.NextRandom(2, 5);
                }

             
                
                
            }


        public void Update(GameTime gameTime)
        {
            
            p.Update(gameTime);
            foreach (ChasingEnemy chaser in chasers)
            {
                chaser.follow(p);
                chaser.Update(gameTime);
            }
            eplatformer.Update(gameTime);
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime)
        {
            p.Draw(spriteBatch);
            eplatformer.Draw(spriteBatch);
            
            foreach (ChasingEnemy chaser in chasers)
                chaser.Draw(spriteBatch);
        }


        
    }
}
