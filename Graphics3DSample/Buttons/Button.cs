using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graphics3DSample.Buttons
{
    /// <summary>
    /// A game component, inherits to Clickable.
    /// Has associated content.
    /// Has an integer Value that is incremented by click.
    /// Draws content.
    /// </summary>
    public class Button : Clickable
    {
        #region Fields

        private readonly string asset;
        private Texture2D texture;
        private int value;

        #region Public accessors

        public int Value { get { return value; } }

        #endregion Public accessors

        #endregion Fields

        #region Initialization

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">The Game object</param>
        /// <param name="textureName">Texture Name</param>
        /// <param name="targetRectangle">Position of the component on the screen</param>
        /// <param name="initialValue">Initial value</param>
        public Button(Graphics3DSampleGame game, string textureName, Rectangle targetRectangle, int initialValue)
            : base(game, targetRectangle)
        {
            asset = textureName;
            value = initialValue;
        }

        /// <summary>
        /// Load the button's texture
        /// </summary>
        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>(asset);
            base.LoadContent();
        }

        #endregion Initialization

        #region Update and render

        /// <summary>
        /// Allows the game component to update itself
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            HandleInput();
            if (IsClicked)
                ++value;
            base.Update(gameTime);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            var color = IsTouching ? Color.Wheat : Color.White;
            Game.SpriteBatch.Begin();
            Game.SpriteBatch.Draw(texture, Rectangle, color);
            Game.SpriteBatch.End();
            base.Draw(gameTime);
        }

        #endregion Update and render
    }
}