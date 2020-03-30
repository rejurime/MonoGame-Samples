using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graphics3DSample.Buttons
{
    /// <summary>
    /// A game component, inherits to Clickable.
    /// Has associated On and Off content.
    /// Has a state of IsChecked that is switched by click.
    /// Draws content according to state.
    /// </summary>
    public class Checkbox : Clickable
    {
        #region Fields

        private readonly string asset;
        private Texture2D textureOn;
        private bool isChecked;

        #region Public accessors

        public bool IsChecked { get { return isChecked; } }

        #endregion Public accessors

        #endregion Fields

        #region Initialization

        /// <summary>
        ///
        /// </summary>
        /// <param name="game">The Game object</param>
        /// <param name="textureName">Texture name</param>
        /// <param name="targetRectangle">Position of the component on the screen</param>
        /// <param name="isChecked">Initial state of the checkbox</param>
        public Checkbox(Graphics3DSampleGame game, string textureName, Rectangle targetRectangle, bool isChecked)
            : base(game, targetRectangle)
        {
            asset = textureName;
            this.isChecked = isChecked;
        }

        /// <summary>
        /// Load the texture
        /// </summary>
        protected override void LoadContent()
        {
            textureOn = Game.Content.Load<Texture2D>(asset);
            base.LoadContent();
        }

        #endregion Initialization

        #region Update and render

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            HandleInput();
            isChecked = IsClicked ? !isChecked : isChecked;
            base.Update(gameTime);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            Game.SpriteBatch.Begin();
            Game.SpriteBatch.Draw(textureOn, Rectangle,
                IsChecked ? Color.Yellow : Color.White);
            Game.SpriteBatch.End();
            base.Draw(gameTime);
        }

        #endregion Update and render
    }
}