using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chess;

public class Game1 : Game
{
    Texture2D ballTexture;
    Texture2D woodTile;
    Vector2 ballPosition;
    float ballSpeed;
    Rectangle[] grid;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Rectangle darkSourceRectangle = new Rectangle(0, 0, 64, 64);
    Rectangle lightSourceRectangle = new Rectangle(0, 72, 64, 64);
    int gridSize = 8;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
            _graphics.PreferredBackBufferHeight / 2);
        ballSpeed = 100f;

        _graphics.IsFullScreen = false;
        _graphics.PreferredBackBufferWidth = 640;
        _graphics.PreferredBackBufferHeight = 530;
        _graphics.ApplyChanges();

        grid = InitGrid();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        ballTexture = Content.Load<Texture2D>("ball");
        woodTile = Content.Load<Texture2D>("WoodTile");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        var kstate = Keyboard.GetState();

        if (kstate.IsKeyDown(Keys.Up))
        {
            ballPosition.Y -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        if (kstate.IsKeyDown(Keys.Down))
        {
            ballPosition.Y += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        if (kstate.IsKeyDown(Keys.Left))
        {
            ballPosition.X -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        if (kstate.IsKeyDown(Keys.Right))
        {
            ballPosition.X += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(
            ballTexture,
            ballPosition,
            null,
            Color.White,
            0f,
            new Vector2(ballTexture.Width / 2, ballTexture.Height / 2),
            Vector2.One,
            SpriteEffects.None,
            0f
        );

        /*  for (int i = 0; i < gridSize; ++i)
          {
              for (int j = 0; j < gridSize; ++j)
              {
                  _spriteBatch.Draw(
               woodTile,
               new Vector2(0+(64*j), 0+(64*i)),
               sourceRectangle,
               Color.White,
               0f,
               Vector2.Zero,
                Vector2.One * 1,
                SpriteEffects.None,
                0f
            );
              }
          }*/


        /*_spriteBatch.Draw(
            woodTile,
            Vector2.Zero,
            lightSourceRectangle,
            Color.White,
            0f,
            Vector2.Zero,
             Vector2.One * 1,
             SpriteEffects.None,
             0f
         );*/

        DrawBoard();
        
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    public Rectangle[] InitGrid()
    {
        Rectangle[] spaces = new Rectangle[gridSize * gridSize];
        for (int i = 0; i < spaces.Length; i++)
        {
            spaces[i] = new Rectangle(i % 8 * 64, (i / 8) * 64, 64, 64);
        }

        return spaces;
    }

    public void DrawBoard()
    {
        for (int i = 0; i < grid.Length; i++)
        {
            _spriteBatch.Draw(woodTile, grid[i],
                ((i%8) + (i/8)) % 2 == 0 ? lightSourceRectangle : darkSourceRectangle, Color.White);
            
        }
    }

}

