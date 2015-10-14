using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Prototype_Menues_etc
{
    /// <summary>
    /// This is the main type for your game
    /// 
    /// Open Source Contributers
    /// OpenGameArt, DualR, Skab
    /// Narrator Ubahootah
    /// 
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        bool firing = false;
        public Player player = new Player();
        //General Game Variables
        public String GameState = "Menu";
        String Difficulty = "Medium";
        TimeSpan LevelLength, GameStart;
        Texture2D VictoryScreen;
        String PlayerAction = "Down";



        //for GUI BUTTONS
        public Button PlayButton = new Button();
        public Texture2D PlayButtonTexture;
        Texture2D OptionButtonTexture;
        Texture2D ExitButtonTexture;
        Texture2D DifficultyButtonTexture;
        Texture2D ContButtTexture;
        Texture2D BackButTex;
        Texture2D MontageText;
        Texture2D TitleImage;
        SoundEffect Start;
        public List<Button> buttons;
        GunAnimation gunAnimation = new GunAnimation();
        Texture2D BG1, BG2, BG3, BGMenu;
        //CutScenes
        Texture2D CS1, CS2, CS3, CS4, Control;
        Song S1, S2, S3, S4;
        int Level = 3;
        int CutSceneCount = 0;
        TimeSpan CutSceneTime; //Ehhhhh

        KeyboardState oldState; //ForKeyboard Controls
        float playerMoveSpeed;
        public int score;
        public int health;

        //Gun Related Variables
        public string CurGun = "revolver";
        public int RevolverAmmo;
        int RevolverLoaded;
        public int WinAmmo;
        int WinLoaded;
        SoundEffect Revolver;
        SoundEffect Winchester;
        SoundEffect ReloadSound;
        bool ChangeGun;

        //Power Up Variables
        int EnemiesKilled;

        TimeSpan MinBlock; //The 60 second basic timer for Power Ups
        TimeSpan PreviousPUSpawn;
        Texture2D powerUpTexture;
        List<PowerUp> powerups;

        // Enemies
        Texture2D enemyTexture;
        List<Enemy> enemies;
        Texture2D toastEnemyTexture;
        List<ToasterEnemy> toastEnemies;
        Texture2D sniperTexture;
        public Texture2D sniperBulletTexture;
        List<SniperEnemy> sniperEnemies;
        public List<SniperProjectile> sniperBullets;

        Texture2D bossToasterTexture;
        //public BossToaster bossToaster;
        BossToaster bossToaster = new BossToaster();
        BossToaster bossToaster2 = new BossToaster();
        //Pam
        PamBoss pamBoss = new PamBoss();
        public Texture2D pamBossTexture;
        public Texture2D pamBossProjectile;
        public Texture2D bossToasterProjectile;
        public List<BossToasterProjectile> bossToasterProjectiles;
        public List<PamBossProjectile> pamBossProjectiles;
        public bool BossSpawn = false;
        public bool BossKill = false;
        int BossSpawnCount = 0;

        public Texture2D EnemyCrosshairTexture;
        public Texture2D EnemyBulletTexture;


        


        // The rate at which the enemies appear
        TimeSpan enemySpawnTime;
        TimeSpan previousSpawnTime;

        TimeSpan toastEnemySpawnTime;
        TimeSpan toastPreviousSpawnTime;

        TimeSpan sniperEnemySpawnTime;
        TimeSpan sniperPreviousSpawnTime;

        // Boss timer?
        public TimeSpan BossSpawnTime;

        // A random number generator
        Random random;
        
        //For Projectile
        Texture2D projectileTexture;
        public List<Projectile> projectiles;

        public Texture2D toastTexture;
        public List<ToastProjectile> ToastProjectiles;


        Texture2D explosionTexture;
        List<Animation> explosions;
        Texture2D gunTexture;

        SpriteFont font;
        //Sounds
        Song GameMusic;
       

        Texture2D msTexture;
        Vector2 mousePosition = Vector2.Zero;
        public Texture2D SniperCrosshair;

        MouseState ms = Mouse.GetState();

        //Montage Mode Sounds + Variables
        Boolean MontageMode = false;
        SoundEffect Hit;
        SoundEffect Wow;
        SoundEffect NoScope;
        SoundEffect Sad;
        SoundEffect Triple;
        SoundEffect Damnson;

        public Game1()  
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            //Player player = new Player(spriteBatch);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            oldState = Keyboard.GetState(); //Sets koyboard state to a basic form
            playerMoveSpeed = 8.0f;
            base.Initialize();

            // Initialize the enemies list
            enemies = new List<Enemy>();
            toastEnemies = new List<ToasterEnemy>();
            sniperEnemies = new List<SniperEnemy>();
            sniperBullets = new List<SniperProjectile>();
            bossToasterProjectiles = new List<BossToasterProjectile>();
            buttons = new List<Button>();
            // Set the time keepers to zero
            previousSpawnTime = TimeSpan.Zero;

            // Used to determine how fast enemy respawns
            //Set to Medium
            enemySpawnTime = TimeSpan.FromSeconds(2.0f);
            toastEnemySpawnTime = TimeSpan.FromSeconds(8.0f);
            sniperEnemySpawnTime = TimeSpan.FromSeconds(15.0f);
            MinBlock = TimeSpan.FromMinutes(0.7f);
            LevelLength = TimeSpan.FromMinutes(0.1f);
            
            CutSceneTime = TimeSpan.FromSeconds(8.0f);
            
            // Initialize our random number generator
            random = new Random();

            //Projectile Code
            projectiles = new List<Projectile>();
            ToastProjectiles = new List<ToastProjectile>();
            powerups = new List<PowerUp>();
            pamBossProjectiles = new List<PamBossProjectile>();
            score = 0;

            //MainGUI
            Button playButton = new Button();
            playButton.Initialize(PlayButtonTexture, new Vector2(1040, 200));
            buttons.Add(playButton);
            Button optionButton = new Button();
            optionButton.Initialize(OptionButtonTexture, new Vector2(1040, 400));
            buttons.Add(optionButton);
            Button exitButton = new Button();
            exitButton.Initialize(ExitButtonTexture, new Vector2(1040, 600));
            buttons.Add(exitButton);

            //Options
            Button difficultyButton = new Button();
            difficultyButton.Initialize(DifficultyButtonTexture, new Vector2(390, 200));
            buttons.Add(difficultyButton);
            Button montageButton = new Button();
            montageButton.Initialize(MontageText, new Vector2(390, 400));
            buttons.Add(montageButton);
            Button optionButton2 = new Button();
            optionButton2.Initialize(BackButTex, new Vector2(390, 600));
            buttons.Add(optionButton2);



            //Dying
            Button continueButton = new Button();
            continueButton.Initialize(ContButtTexture, new Vector2(600, 518));
            buttons.Add(continueButton);
            Button exitButton2 = new Button();
            exitButton2.Initialize(ExitButtonTexture, new Vector2(900, 518));
            buttons.Add(exitButton2);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 


        Vector2 spriteSpeed = new Vector2(50.0f, 50.0f);

        protected override void LoadContent()
        {
            Content.RootDirectory = "Content";
            
            //Handle the button creation
             
            // Create a new SpriteBatch, which can be used to draw textures.
            //Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            Animation playerAnimation = new Animation();
            Texture2D playerTexture = Content.Load<Texture2D>("Ryen4");
            playerAnimation.Initialize(playerTexture, 0, 50, 50);
            Vector2 playerPosition = new Vector2(640, 360);
            //GUI
            BackButTex = Content.Load<Texture2D>("BackButton");
            PlayButtonTexture = Content.Load<Texture2D>("PlayButton");
            ExitButtonTexture = Content.Load<Texture2D>("ExitButton");
            OptionButtonTexture = Content.Load<Texture2D>("OptionsButton");
            ContButtTexture = Content.Load<Texture2D>("Continue");
            DifficultyButtonTexture = Content.Load<Texture2D>("Difficulty");
            MontageText = Content.Load<Texture2D>("Montage");


            player.Initialize(playerAnimation, playerPosition);
            VictoryScreen = Content.Load<Texture2D>("Victory");
            enemyTexture = Content.Load<Texture2D>("EnemyMoveDodge Test");
            projectileTexture = Content.Load<Texture2D>("Bullet");
            msTexture = Content.Load<Texture2D>("CrossHair1");
            explosions = new List<Animation>();
            explosionTexture = Content.Load<Texture2D>("EnemyKill");
            font = Content.Load<SpriteFont>("gameFont");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Revolver = Content.Load<SoundEffect>("Colt");
            Winchester = Content.Load<SoundEffect>("Winchester");
            ReloadSound = Content.Load<SoundEffect>("Reload");
            GameMusic = Content.Load<Song>("BeforeDawn");
            Start = Content.Load<SoundEffect>("Start2");
            toastTexture = Content.Load<Texture2D>("ToastProjectile");
            toastEnemyTexture = Content.Load<Texture2D>("ToasterEnemy");
            EnemyCrosshairTexture = Content.Load<Texture2D>("EnemyCrosshair");
            EnemyBulletTexture = Content.Load <Texture2D>("SniperBulletTexture");
            sniperTexture = Content.Load<Texture2D>("Sniper");
            powerUpTexture = Content.Load<Texture2D>("PowerUps");
            TitleImage = Content.Load<Texture2D>("TitleScreen");
            BG1 = Content.Load<Texture2D>("BackGround1");
            S1 = Content.Load<Song>("Scene1");
            BG2 = Content.Load<Texture2D>("BackGround2");
            S2 = Content.Load<Song>("Scene2");
            BG3 = Content.Load<Texture2D>("BackGround3");
            S3 = Content.Load<Song>("Scene3");
            BGMenu = Content.Load<Texture2D>("WesterTown");
            S4 = Content.Load<Song>("Scene4");
            //CutScenes
            CS1 = Content.Load<Texture2D>("BreadStall");
            CS2 = Content.Load<Texture2D>("kidnap");
            CS3 = Content.Load<Texture2D>("tied up");
            CS4 = Content.Load<Texture2D>("factory");
            Control = Content.Load<Texture2D>("Control Scheme");
            bossToasterTexture = Content.Load<Texture2D>("BossToaster");
            bossToasterProjectile = Content.Load<Texture2D>("BossProj");
            pamBossProjectile = Content.Load<Texture2D>("PamProj");
            pamBossTexture = Content.Load<Texture2D>("PamBoss");
            //Montage Load
            Hit = Content.Load<SoundEffect>("HIT");
            Wow = Content.Load<SoundEffect>("WOW");
            NoScope = Content.Load<SoundEffect>("NOSCOPE");
            Sad = Content.Load<SoundEffect>("2SAD");
            Triple = Content.Load<SoundEffect>("TRIPLE");
            Damnson = Content.Load<SoundEffect>("DAMNSON");
            // Create the gun object

            RevolverLoaded = 6;
            RevolverAmmo = 60;
            WinLoaded = 15;
            WinAmmo = 120;
            // Initialize the animation with the correct animation information

            

            

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        /// 
        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

 
            spriteBatch.Begin();
             // Draw the Player
            if (GameState == "Menu")
            {
                spriteBatch.Draw(BGMenu, new Vector2(190,62), Color.White);
                spriteBatch.Draw(TitleImage, new Vector2(300, 100), Color.White);
                for (int i = 0; i < 3; i++)
                {
                    buttons[i].Draw(spriteBatch);
                }

            }
            else if (GameState == "Options")
            {
                spriteBatch.Draw(BGMenu, new Vector2(190,62), Color.White);
                for (int i = 3; i < 6; i++)
                {
                    buttons[i].Draw(spriteBatch);
                    spriteBatch.DrawString(font, "Difficulty: " + Difficulty, new Vector2(400, 140), Color.White);
                    if (MontageMode == true)
                    {
                        spriteBatch.DrawString(font, "Montage on!" , new Vector2(400, 340), Color.White);
                    }
                }

            }
            else if (GameState == "Dying")
            {
                switch (Level) // Draws Level for Background, otherwise boring
                {
                    case 1:
                        spriteBatch.Draw(BG1, new Vector2(0, 0), Color.White);
                        break;
                    case 2:
                        spriteBatch.Draw(BG2, new Vector2(0, 0), Color.White);
                        break;
                    case 3:
                        spriteBatch.Draw(BG3, new Vector2(0, 0), Color.White);
                        break;
                }
                for (int i = 6; i < 8; i++) // Gonna draw just two buttons. One will exit the game, one will restart the level
                {
                    buttons[i].Draw(spriteBatch);
                    spriteBatch.DrawString(font, "Restart or Exit?", new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 550, GraphicsDevice.Viewport.TitleSafeArea.Y + 100), Color.Black);
                    //spriteBatch.DrawString(font, "Difficulty: " + Difficulty, new Vector2(300, 140), Color.White);
                }

            }
            else if (GameState == "Victory")
            {
                
                spriteBatch.Draw(BG3, new Vector2(0, 0), Color.White);
                player.Draw(spriteBatch);
                pamBoss.Draw(spriteBatch);
                spriteBatch.Draw(VictoryScreen, new Vector2(400,200), Color.White);
                buttons[2].Draw(spriteBatch);
            }
            else if (GameState == "Cutscene")
            {
                if (gameTime.TotalGameTime - GameStart >= CutSceneTime)
                {
                    CutSceneCount++;
                    switch (CutSceneCount)
                    {
                        case 1:
                            MediaPlayer.IsRepeating = false;
                            MediaPlayer.Play(S1);
                            CutSceneTime = TimeSpan.FromSeconds(9.0f);
                            break;
                        case 2:
                            MediaPlayer.Play(S2);
                            CutSceneTime = TimeSpan.FromSeconds(9.0f);
                            break;
                        case 3:
                            MediaPlayer.Play(S3);
                            CutSceneTime = TimeSpan.FromSeconds(9.0f);
                            break;
                        case 4:
                            if (Level > 2)
                            {
                               //S4.Play();
                                CutSceneTime = TimeSpan.FromSeconds(9.0f);
                            }
                            break;

                    }
                    GameStart = gameTime.TotalGameTime;
                    if (Level == 1 && CutSceneCount > 3)
                    {
                        GameState = "Playing";
                        PlayMusic(GameMusic);
                    }
                    else if (Level == 3 && CutSceneCount > 4)
                    {
                        GameState = "Playing";
                        MediaPlayer.Play(GameMusic);
                        MediaPlayer.IsRepeating = true;
                        GameStart = gameTime.TotalGameTime;
                    }

                }
                switch (CutSceneCount)
                {
                    case 1: // were all 0,0
                        spriteBatch.Draw(CS1, new Vector2(40, 0), Color.White);
                        break;
                    case 2:
                        spriteBatch.Draw(CS2, new Vector2(190, 68), Color.White);
                        break;
                    case 3:
                        spriteBatch.Draw(CS3, new Vector2(190, 30), Color.White);
                        break;
                    case 4:
                        spriteBatch.Draw(CS4, new Vector2(190, 32), Color.White);
                        //S4.Play();
                        break;
                    case 0: // were all 0,0
                        spriteBatch.Draw(Control, new Vector2(0, 0), Color.White);
                        break;
                }




                //ends with turning gameState to Playing
                //GameState = "Playing";
            }
            else if (GameState == "Playing")
            {
                switch (Level)
                {
                    case 1:
                        spriteBatch.Draw(BG1, new Vector2(0, 0), Color.White);
                        break;
                    case 2:
                        spriteBatch.Draw(BG2, new Vector2(0, 0), Color.White);

                        break;
                    case 3:
                        spriteBatch.Draw(BG3, new Vector2(0, 0), Color.White);
                        break;

                }

                player.Draw(spriteBatch);

                if (BossSpawn == false)
                {
                    //Will Draw Gui

                    for (int i = 0; i < enemies.Count; i++)
                    {
                        enemies[i].Draw(spriteBatch);
                    }

                    for (int i = 0; i < sniperEnemies.Count; i++)
                    {
                        sniperEnemies[i].Draw(spriteBatch);
                    }

                    for (int i = 0; i < toastEnemies.Count; i++)
                    {
                        toastEnemies[i].Draw(spriteBatch);
                    }

                    for (int i = 0; i < powerups.Count; i++)
                    {
                        powerups[i].Draw(spriteBatch);
                    }

                    // Draw the Projectiles
                    for (int i = 0; i < projectiles.Count; i++)
                    {
                        projectiles[i].Draw(spriteBatch);
                    }

                    for (int i = 0; i < ToastProjectiles.Count; i++)
                    {
                        ToastProjectiles[i].Draw(spriteBatch);
                    }
                    for (int i = 0; i < sniperBullets.Count; i++)
                    {
                        sniperBullets[i].Draw(spriteBatch);
                    }
                }
                else
                {
                    for (int i = 0; i < bossToasterProjectiles.Count; i++)
                    {
                        bossToasterProjectiles[i].Draw(spriteBatch);
                    }
                    for (int i = 0; i < pamBossProjectiles.Count; i++)
                    {
                        pamBossProjectiles[i].Draw(spriteBatch);
                    }
                    if (Level == 1)
                        bossToaster.Draw(spriteBatch);
                    if (Level == 2)
                        bossToaster2.Draw(spriteBatch);
                    if (Level == 3)
                        pamBoss.Draw(spriteBatch);
                    for (int i = 0; i < powerups.Count; i++)
                    {
                        powerups[i].Draw(spriteBatch);
                    }

                    // Draw the Projectiles
                    for (int i = 0; i < projectiles.Count; i++)
                    {
                        projectiles[i].Draw(spriteBatch);
                    }

                }

                // Draw the explosions
                // Draw the score
                spriteBatch.DrawString(font, "score: " + score, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White);
                // Draw the player health
                spriteBatch.DrawString(font, "health: " + player.Health, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + 30), Color.White);
                if (CurGun == "revolver")
                {
                    spriteBatch.DrawString(font, "Revolver: " + RevolverLoaded + "/" + RevolverAmmo + " <-", new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + 60), Color.White);
                    spriteBatch.DrawString(font, "Winchester: " + WinLoaded + "/" + WinAmmo, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + 90), Color.White);
                }
                else if (CurGun == "winchester")
                {
                    spriteBatch.DrawString(font, "Revolver: " + RevolverLoaded + "/" + RevolverAmmo, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + 60), Color.White);
                    spriteBatch.DrawString(font, "Winchester: " + WinLoaded + "/" + WinAmmo + " <-", new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + 90), Color.White);
                }
                // Draw the player health
                // Stop drawing




                gunAnimation.Update(gameTime, 100, 100, CurGun);
            } // End Playing
            spriteBatch.Draw(msTexture, new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 12, 14), Color.White); 
           
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void UpdateInput()
        {
            KeyboardState newState = Keyboard.GetState();

            // Update saved state.
            oldState = newState;
        }

        protected override void Update(GameTime gameTime)
        {
            oldState = Keyboard.GetState();
            mousePosition.X = Mouse.GetState().X;
            mousePosition.Y = Mouse.GetState().Y;//Logical to have these 3 lines first to me
            UpdatePlayer(gameTime);
            if (GameState == "Menu" || GameState == "Options" || GameState == "Dying" || GameState == "Victory")
            {
                //Unneeded?
                //UpdateButtons();


            }
            else if (GameState == "Playing")
            {
                if (gameTime.TotalGameTime - GameStart >= LevelLength)
                {
                    BossSpawn = true;
                    UpdateBoss(gameTime);
                    Despawn();
                    //Chill on this
                    if (BossKill == true)
                    {
                        BossKill = false;
                        BossSpawnCount = 0;
                        if (MontageMode == true)
                        {
                            Wow.Play();
                        }
                        
                        Level++;
                        if (Level == 3)
                        {
                            GameState = "Cutscene";
                            MediaPlayer.Stop();
                            MediaPlayer.Play(S4);
  
                        }

                        GameStart = gameTime.TotalGameTime;
                        BossSpawn = false;
                    }
                }
                UpdateEnemies(gameTime);
                UpdateCollision();
                UpdatePowerUps(gameTime);
                UpdateProjectiles(gameTime);
                gunAnimation.Update(gameTime, 100, 100, CurGun); // Doesn't Work
                //UpdateExplosions(gameTime);
            }

            
        }
        
        //private void UpdateButtons()
        //{
        //    for (int i = buttons.Count - 1; i >= 0; i--)
        //    {
        //        buttons[i].Update(mousePosition);
        //    }

        //}
        private void UpdatePlayer(GameTime gameTime)
        {
            //String Action = "Right";
            var mosState = Mouse.GetState();
            if (GameState == "Menu" || GameState == "Options" || GameState == "Dying" || GameState == "Victory")
            {
                if (mosState.LeftButton == ButtonState.Pressed && firing == false)
                {
                    MenuCollision(gameTime);
                    firing = true;
                }
                if (mosState.LeftButton == ButtonState.Released)
                {
                    firing = false;
                }
            }
            if (GameState == "Cutscene")
            {

                if (oldState.IsKeyDown(Keys.Escape)){
                    CutSceneCount = 4;
                     MediaPlayer.Stop();
                     GameState = "Playing";
                     MediaPlayer.Play(GameMusic);
                     MediaPlayer.IsRepeating = true;
                }

            } // End CutStop
            
            if (GameState == "Playing")
            {
                
                // Use the Keyboard / Dpad
                if (oldState.IsKeyDown(Keys.A) && player.Health >= 0)
                {
                    player.Position.X -= playerMoveSpeed;
                    PlayerAction = "Left";
                }
                if (oldState.IsKeyDown(Keys.D) && player.Health >= 0)
                {
                    player.Position.X += playerMoveSpeed;
                    PlayerAction = "Right";
                }
                if (oldState.IsKeyDown(Keys.W) && player.Health >= 0)
                {
                    player.Position.Y -= playerMoveSpeed;
                    PlayerAction = "Up";
                }
                if (oldState.IsKeyDown(Keys.S) && player.Health >= 0)
                {
                    player.Position.Y += playerMoveSpeed;
                    PlayerAction = "Down";
                }
                if (oldState.IsKeyDown(Keys.E) && player.Health >= 0)
                {
                    if (ChangeGun == false)
                    {
                        ChangeGuns();
                        ChangeGun = true;
                    }
                }
                if (oldState.IsKeyUp(Keys.E))
                {
                    ChangeGun = false;
                }
                if (oldState.IsKeyDown(Keys.R) && player.Health >= 0)
                {
                    Reload(gameTime);
                }
                // Make sure that the player does not go out of bounds
                player.Position.X = MathHelper.Clamp(player.Position.X, 0, GraphicsDevice.Viewport.Width - player.Width / 2);
                player.Position.Y = MathHelper.Clamp(player.Position.Y, 0, GraphicsDevice.Viewport.Height - player.Height / 2);

                
                if (mosState.LeftButton == ButtonState.Pressed)
                {
                    if (firing == false)
                    {
                        if (CurGun == "revolver")
                        {
                            if (RevolverLoaded >= 1)
                            {
                                AddProjectile(player.Position);
                                firing = true;
                                Revolver.Play();
                                RevolverLoaded -= 1;
                            }
                        }
                        else
                        {
                            if (WinLoaded >= 1)
                            {
                                AddProjectile(player.Position);
                                firing = true;
                                Winchester.Play();
                                WinLoaded -= 1;
                            }

                        } //end curgun
                    } // end firing
                }
                if (mosState.LeftButton == ButtonState.Released)
                {
                    firing = false;
                }

                // Fire only every interval we set as the fireTime
                //if (gameTime.TotalGameTime - previousFireTime > fireTime)
                //{
                //    // Reset our current time
                //    previousFireTime = gameTime.TotalGameTime;

                //    // Add the projectile, but add it to the front and center of the player
                //    AddProjectile(player.Position + new Vector2(player.Width / 2, 0));
                //}
                if (player.Health <= 0)
                {
                    if (MontageMode == true)
                    {
                        Sad.Play();
                        
                    }
                    PlayerAction = "Dying";
                    GameState = "Dying";
                    
                }
                player.Update(gameTime, PlayerAction);

            }
        }

        //Self Added



        private void UpdatePowerUps(GameTime gameTime)
        {
            /// <summary>
            /// Spawns a power Up every 7 enemy kills or whether 1 minute has passed in game time.
            /// </summary>
            /// 

            //One minute timer
            if (gameTime.TotalGameTime - PreviousPUSpawn >= MinBlock)
            {
                PreviousPUSpawn = gameTime.TotalGameTime;
                //Add a power up
                AddPowerUp(gameTime);
                if (MontageMode == true)
                    Damnson.Play();
            }
            if (EnemiesKilled >= 7)
            {
                EnemiesKilled = 0;
                if(MontageMode == true)
                    Triple.Play();
                AddPowerUp(gameTime);
            }
            

            for (int i = powerups.Count - 1; i >= 0; i--)
            {
                powerups[i].Update(gameTime);

                if (powerups[i].Active == false)
                {
                    powerups.RemoveAt(i);
                }
            }
            
        }


        private void ChangeGuns()
        {

            if (CurGun == "revolver")
                CurGun = "winchester";
            else
                CurGun = "revolver";
        }

        private void Reload(GameTime gameTime)
        {
            //reloads guns up until their maximum, 6 and 15
            
            if (CurGun == "revolver")
            {
                if (RevolverLoaded < 6 && RevolverAmmo > 0)
                {
                    ReloadSound.Play();
                    if (RevolverAmmo - (6 - RevolverLoaded) >= 0)
                    {
                        RevolverAmmo = RevolverAmmo - (6 - RevolverLoaded);
                        RevolverLoaded = 6;
                        
                    }
                    else
                    {
                        RevolverLoaded = RevolverAmmo;
                        RevolverAmmo = 0;
                    }
                }
            }
            else if (CurGun == "winchester")
            {
                if (WinLoaded < 15 && WinAmmo > 0)
                {
                    ReloadSound.Play();
                    if (WinAmmo - (15 - WinLoaded) >= 0)
                    {
                        WinAmmo = WinAmmo - (15 - WinLoaded);
                        WinLoaded = 15;
                    }
                    else
                    {
                        WinLoaded = WinAmmo;
                        WinAmmo = 0;
                    }
                }

            }
        }



        private void AddPowerUp(GameTime gameTime)
        {
            PowerUpAnimation powerUpAnimation = new PowerUpAnimation();
            powerUpAnimation.Initialize(powerUpTexture, 0, 50, 50);
            Vector2 position = new Vector2(random.Next(100, GraphicsDevice.Viewport.Width - 50), random.Next(100, GraphicsDevice.Viewport.Height - 50));
            PowerUp powerup = new PowerUp();
            powerup.Initialize(powerUpAnimation, position, gameTime);
            powerups.Add(powerup);


        }
        private void AddEnemy()
        {
            // Create the animation object
            EnemyAnimation enemyAnimation = new EnemyAnimation();

            // Initialize the animation with the correct animation information
            enemyAnimation.Initialize(enemyTexture, 0, 50, 50);

            // Randomly generate the position of the enemy
            Vector2 position = new Vector2(GraphicsDevice.Viewport.Width + enemyTexture.Width / 2, random.Next(100, GraphicsDevice.Viewport.Height));

            // Create an enemy
            Enemy enemy = new Enemy();

            // Initialize the enemy
            enemy.Initialize(enemyAnimation, position);

            // Add the enemy to the active enemies list
            enemies.Add(enemy);
        }

        private void AddSniperEnemy(GameTime gameTime)
        {
            Random SniperRandom = new Random();
            SniperEnemyAnimation sniperEnemyAnimation = new SniperEnemyAnimation();
            TimeSpan SpawnTime = gameTime.TotalGameTime;
            sniperEnemyAnimation.Initialize(sniperTexture, 0, 50, 50);
            Vector2 position = new Vector2(random.Next(100, GraphicsDevice.Viewport.Width - 50), random.Next(100, GraphicsDevice.Viewport.Height - 50));
            Vector2 target = new Vector2 (SniperRandom.Next(0,1000), SniperRandom.Next(0,800));
            SniperEnemy sniperEnemy = new SniperEnemy();
            sniperEnemy.Initialize(sniperEnemyAnimation, position, target);
            sniperEnemies.Add(sniperEnemy);

        }
        private void AddToastEnemy()
        {
            //Create animation Object
            string direction = "";
            Random Rando = new Random();
            int dir = Rando.Next(0, 4);
            switch (dir)
            {
                case 0:
                    direction = "Top";
                    break;
                case 1:
                    direction = "Left";
                    break;
                case 2:
                    direction = "Bottom";
                    break;
                case 3:
                    direction = "Right";
                    break;

            }
            ToasterAnimation toastAnimation = new ToasterAnimation();

            toastAnimation.Initialize(toastEnemyTexture, 0, 50, 100);

            Vector2 toastPosition = new Vector2(GraphicsDevice.Viewport.Width - toastEnemyTexture.Width / 2, random.Next(100, GraphicsDevice.Viewport.Height)); // bad work
            
            ToasterEnemy toastEnemy = new ToasterEnemy();
            toastEnemy.Initialize(toastAnimation, toastPosition, direction);
            toastEnemies.Add(toastEnemy);

        }

        private void AddProjectile(Vector2 position)
        {

            ms = Mouse.GetState();
            Projectile projectile = new Projectile();
            projectile.Initialize(GraphicsDevice.Viewport, projectileTexture, position, new Vector2(ms.X,ms.Y));
            projectiles.Add(projectile);
        }

        public void UpdateBoss(GameTime gameTime)
        {
            if (BossSpawnCount == 0)
            {
                if (Level == 1)
                {
                    BossToasterAnimation bossToasterAnimation = new BossToasterAnimation();
                    bossToasterAnimation.Initialize(bossToasterTexture, 0, 154, 151);
                    Vector2 position = new Vector2(500, 500);

                    bossToaster.Initialize(bossToasterAnimation, position);
                    BossSpawnCount = 1;
                }
                else if (Level == 2)
                {
                    BossToasterAnimation bossToasterAnimation2 = new BossToasterAnimation();
                    bossToasterAnimation2.Initialize(bossToasterTexture, 0, 154, 151);
                    Vector2 position = new Vector2(500, 500);

                    bossToaster2.Initialize(bossToasterAnimation2, position);
                    BossSpawnCount = 1;
                }
                else if (Level == 3)
                {
                    PamBossAnimation pamBossAnimation = new PamBossAnimation();
                    pamBossAnimation.Initialize(pamBossTexture, 0, 50, 100);
                    Vector2 position = new Vector2(500, 500);
                    
                    pamBoss.Initialize(pamBossAnimation, position);
                    BossSpawnCount = 1;
                }
            }
            if(Level == 1)
                bossToaster.Update(gameTime, this);
            if(Level == 2)
                bossToaster2.Update(gameTime, this);
            if (Level == 3)
                pamBoss.Update(gameTime, this);
        
        
        }

        private void UpdateEnemies(GameTime gameTime)
        {
            if (BossSpawn == false)
            {
                // Spawn a new enemy enemy every 1.5 seconds
                if (gameTime.TotalGameTime - previousSpawnTime > enemySpawnTime)
                {
                    previousSpawnTime = gameTime.TotalGameTime;

                    // Add an Enemy
                    AddEnemy();
                }

                if (gameTime.TotalGameTime -  toastPreviousSpawnTime > toastEnemySpawnTime)
                {
                    toastPreviousSpawnTime = gameTime.TotalGameTime;

                    // Add an Enemy
                    AddToastEnemy();
                }

                if (gameTime.TotalGameTime - sniperPreviousSpawnTime > sniperEnemySpawnTime)
                {
                    sniperPreviousSpawnTime = gameTime.TotalGameTime;

                    // Add an Enemy
                    AddSniperEnemy(gameTime);
                }
            }

            // Update the Enemies
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update(gameTime, this);

                if (enemies[i].Active == false)
                {
                    // If not active and health <= 0
                    if (enemies[i].Health <= 0)
                    {
                        EnemiesKilled += 1;
                        //score += enemies[i].Value;
                    }
                    EnemiesKilled += 1;
                    enemies.RemoveAt(i);
                }
            }
            for (int i = toastEnemies.Count - 1; i >= 0; i--)
            {
                toastEnemies[i].Update(gameTime, this );

                if (toastEnemies[i].Active == false)
                {
                    // If not active and health <= 0
                    if (toastEnemies[i].Health <= 0)
                    {
                        EnemiesKilled += 1;
                        //score += enemies[i].Value;
                    }
                    EnemiesKilled += 1;
                    toastEnemies.RemoveAt(i);
                }
            }

            for (int i = sniperEnemies.Count - 1; i >= 0; i--)
            {
                sniperEnemies[i].Update(gameTime, this);

                if (sniperEnemies[i].Active == false)
                {
                    // If not active and health <= 0
                    if (sniperEnemies[i].Health <= 0)
                    {
                        EnemiesKilled += 1;
                        //score += enemies[i].Value;
                    }
                    EnemiesKilled += 1;
                    sniperEnemies.RemoveAt(i);
                }
            }
        }

        private void MenuCollision(GameTime gameTime)
        {
            Rectangle rectangle1;
            Rectangle rectangle2;
            //Player Collisions
            rectangle1 = new Rectangle((int)mousePosition.X,
                        (int)mousePosition.Y,
                        msTexture.Width, msTexture.Height);
            for (int i = 0; i < buttons.Count; i++) // Need to lock it by Options
            {
                rectangle2 = new Rectangle((int)buttons[i].Position.X - buttons[i].Width,
                (int)buttons[i].Position.Y - buttons[i].Height,
                buttons[i].Width,
                buttons[i].Height);

                // Determine if the two objects collided with each
                // other
                if (rectangle1.Intersects(rectangle2))
                {
                    switch (i)
                    {
                        case 0:
                            if (GameState == "Menu")
                            {
                                Start.Play();
                                GameState = "Cutscene";
                                GameStart = gameTime.TotalGameTime;
                            }
                            break;
                        case 1:
                            if (GameState == "Menu")
                            {
                                GameState = "Options";
                            }
                            break;
                        case 2:
                            if (GameState == "Menu" || GameState == "Victory")
                            {
                                Exit();
                            }
                            break;
                        case 3:
                            if (GameState == "Options")
                            {
                                ChangeDifficulty();
                            }
                            break;
                        case 4:
                            if (GameState == "Options")
                            {
                                if (MontageMode == false)
                                {
                                    MontageMode = true;
                                }
                                else
                                {
                                    MontageMode = false;
                                }
                            }
                            break;
                        case 5:
                            if (GameState == "Options")
                            {
                                GameState = "Menu";
                            }
                            break;
                        
                        case 6:
                            if (GameState == "Dying")
                            {
                                GameState = "Playing";
                                player.Health = 100;
                                score = 0;
                                //GameStart = gameTime.TotalGameTime;
                                Despawn();
                                Death(gameTime);
                            }
                            break;
                        case 7:
                            if (GameState == "Dying")
                            {
                                Exit();
                            }
                            break;
                    }
                }

            }

        }

        private void ChangeDifficulty()
        {
            switch (Difficulty)
            {
                // Used to determine how fast enemy respawns
                case "Medium":
                    Difficulty = "Hard";
                    enemySpawnTime = TimeSpan.FromSeconds(1.0f);
                    toastEnemySpawnTime = TimeSpan.FromSeconds(6.0f);
                    sniperEnemySpawnTime = TimeSpan.FromSeconds(10.0f);
                    MinBlock = TimeSpan.FromMinutes(1.0f);
                    LevelLength = TimeSpan.FromMinutes(7.0f);
                    
                    break;
                case "Hard":
                    Difficulty = "Easy";
                    enemySpawnTime = TimeSpan.FromSeconds(3.0f);
                    toastEnemySpawnTime = TimeSpan.FromSeconds(10.0f);
                    sniperEnemySpawnTime = TimeSpan.FromSeconds(20.0f);
                    MinBlock = TimeSpan.FromMinutes(0.4f);
                    LevelLength = TimeSpan.FromMinutes(3.0f);
                    break;
                case "Easy":
                    Difficulty = "Medium";
                    enemySpawnTime = TimeSpan.FromSeconds(2.0f);
                    toastEnemySpawnTime = TimeSpan.FromSeconds(8.0f);
                    sniperEnemySpawnTime = TimeSpan.FromSeconds(15.0f);
                    MinBlock = TimeSpan.FromMinutes(0.7f);
                    LevelLength = TimeSpan.FromMinutes(5.0f);
                    break;
            }
            
        }

        private void UpdateCollision()
        {
            Rectangle rectangle1;
            Rectangle rectangle2;
            //Player Collisions
            rectangle1 = new Rectangle((int)player.Position.X,
                        (int)player.Position.Y,
                        player.Width,
                        player.Height);
            for (int i = 0; i < enemies.Count; i++)
            {
                rectangle2 = new Rectangle((int)enemies[i].Position.X,
                (int)enemies[i].Position.Y,
                enemies[i].Width,
                enemies[i].Height);

                // Determine if the two objects collided with each
                // other
                if (rectangle1.Intersects(rectangle2))
                {
                    // Subtract the health from the player based on
                    // the enemy damage
                    player.Health -= enemies[i].Damage;

                    // Since the enemy collided with the player
                    // destroy it
                    enemies[i].Health = 0;

                    // If the player health is less than zero we died

                    //if (player.Health <= 0)
                    //    player.Active = false;
                }

            }
            for (int i = 0; i < powerups.Count; i++)
            {
                rectangle2 = new Rectangle((int)powerups[i].Position.X,
                (int)powerups[i].Position.Y,
                powerups[i].Width,
                powerups[i].Height);

                // Determine if the two objects collided with each
                // other
                if (rectangle1.Intersects(rectangle2))
                {
                    //Call the pickup effect
                    powerups[i].PickUp(this);
                    
                }

            }



            // Projectile vs Enemy Collision
            for (int i = 0; i < projectiles.Count; i++)
            {
                // Create the rectangles we need to determine if we collided with each other
                rectangle1 = new Rectangle((int)projectiles[i].Position.X -
                projectiles[i].Width / 2, (int)projectiles[i].Position.Y -
                projectiles[i].Height / 2, projectiles[i].Width, projectiles[i].Height);

                for (int j = 0; j < enemies.Count; j++)
                {

                    rectangle2 = new Rectangle((int)enemies[j].Position.X - enemies[j].Width / 2,
                    (int)enemies[j].Position.Y - enemies[j].Height / 2,
                    enemies[j].Width, enemies[j].Height);

                    // Determine if the two objects collided with each other
                    if (rectangle1.Intersects(rectangle2))
                    {
                        enemies[j].Health -= projectiles[i].Damage;
                        projectiles[i].Active = false;
                        if (MontageMode == true)
                        {
                            Hit.Play();
                        }
                    }
                }
                for (int j = 0; j < toastEnemies.Count; j++)
                {

                    rectangle2 = new Rectangle((int)toastEnemies[j].Position.X - toastEnemies[j].Width / 2,
                    (int)toastEnemies[j].Position.Y - toastEnemies[j].Height / 2,
                    toastEnemies[j].Width, toastEnemies[j].Height);

                    // Determine if the two objects collided with each other
                    if (rectangle1.Intersects(rectangle2))
                    {
                        toastEnemies[j].Health -= projectiles[i].Damage;
                        projectiles[i].Active = false;
                        if (MontageMode == true)
                        {
                            Hit.Play();
                        }
                    }
                }
                for (int j = 0; j < sniperEnemies.Count; j++)
                {

                    rectangle2 = new Rectangle((int)sniperEnemies[j].Position.X - 100 / 2,
                    (int)sniperEnemies[j].Position.Y - 50 / 2,
                    100, 50);

                    // Determine if the two objects collided with each other
                    if (rectangle1.Intersects(rectangle2))
                    {
                        sniperEnemies[j].Health -= projectiles[i].Damage;
                        projectiles[i].Active = false;
                        if (MontageMode == true)
                        {
                            Hit.Play();
                        }
                    }
                }

                if (bossToaster.Active == true) // HOLD ALL OF TOASTER BOSS COLLISION for player bullets
                {
                    //Allows the Boss to be hit by Projectiles
                    rectangle2 = new Rectangle(0, 0, 0, 0);
                    if (Level == 1)
                        rectangle2 = new Rectangle((int)bossToaster.Position.X - bossToaster.Width / 2, (int)bossToaster.Position.Y - bossToaster.Height / 2, bossToaster.Width, bossToaster.Height);

                    if (rectangle1.Intersects(rectangle2))
                    {
                        bossToaster.Health -= projectiles[i].Damage;
                        projectiles[i].Active = false;
                    }

                    for (int j = 0; j < bossToasterProjectiles.Count; j++)
                    {
                        //Allows players bullets to kill the toast
                        // Create the rectangles we need to determine if we collided with each other
                        rectangle2 = new Rectangle((int)bossToasterProjectiles[j].Position.X -
                        bossToasterProjectiles[j].Width / 2, (int)bossToasterProjectiles[j].Position.Y -
                        bossToasterProjectiles[j].Height / 2, bossToasterProjectiles[j].Width, bossToasterProjectiles[j].Height);

                        if (rectangle1.Intersects(rectangle2))
                        {
                            projectiles[i].Active = false;
                            bossToasterProjectiles[j].Active = false;
                            EnemiesKilled += 1;
                            if (MontageMode == true)
                            {
                                Hit.Play();
                            }
                        }
                    }

                }
                if (bossToaster2.Active == true) // HOLD ALL OF TOASTER BOSS COLLISION for player bullets
                {
                    //Allows the Boss to be hit by Projectiles
                   
                    
                    rectangle2 = new Rectangle((int)bossToaster2.Position.X - bossToaster2.Width / 2, (int)bossToaster2.Position.Y - bossToaster2.Height / 2, bossToaster2.Width, bossToaster2.Height);

                    if (rectangle1.Intersects(rectangle2))
                    {
                        bossToaster2.Health -= projectiles[i].Damage;
                        projectiles[i].Active = false;
                    }

                    for (int j = 0; j < bossToasterProjectiles.Count; j++)
                    {
                        //Allows players bullets to kill the toast
                        // Create the rectangles we need to determine if we collided with each other
                        rectangle2 = new Rectangle((int)bossToasterProjectiles[j].Position.X -
                        bossToasterProjectiles[j].Width / 2, (int)bossToasterProjectiles[j].Position.Y -
                        bossToasterProjectiles[j].Height / 2, bossToasterProjectiles[j].Width, bossToasterProjectiles[j].Height);

                        if (rectangle1.Intersects(rectangle2))
                        {
                            projectiles[i].Active = false;
                            bossToasterProjectiles[j].Active = false;
                            EnemiesKilled += 1;
                            if (MontageMode == true)
                            {
                                Hit.Play();
                            }
                        }
                    }
                }
                if (pamBoss.Active == true) // HOLDS PAMS COLLISION
                {
                    //Allows the Boss to be hit by Projectiles


                    rectangle2 = new Rectangle((int)pamBoss.Position.X - pamBoss.Width / 2, (int)pamBoss.Position.Y - pamBoss.Height / 2, pamBoss.Width, pamBoss.Height);

                    if (rectangle1.Intersects(rectangle2))
                    {
                        pamBoss.Health -= projectiles[i].Damage;
                        projectiles[i].Active = false;
                    }

                    for (int j = 0; j < pamBossProjectiles.Count; j++)
                    {
                        //Allows players bullets to kill the toast
                        // Create the rectangles we need to determine if we collided with each other
                        rectangle2 = new Rectangle((int)pamBossProjectiles[j].Position.X -
                        pamBossProjectiles[j].Width / 2, (int)pamBossProjectiles[j].Position.Y -
                        pamBossProjectiles[j].Height / 2, pamBossProjectiles[j].Width, pamBossProjectiles[j].Height);

                        if (rectangle1.Intersects(rectangle2))
                        {
                            projectiles[i].Active = false;
                            pamBossProjectiles[j].Active = false;
                            EnemiesKilled += 1;
                            if (MontageMode == true)
                            {
                                Hit.Play();
                            }
                        }
                    }
                }
            }
            // ToastProjectiles vs Player Collision
            rectangle2 = new Rectangle((int)player.Position.X,
                       (int)player.Position.Y,
                       player.Width,
                       player.Height);
            for (int i = 0; i < ToastProjectiles.Count; i++)
            {
                
                    // Create the rectangles we need to determine if we collided with each other
                    rectangle1 = new Rectangle((int)ToastProjectiles[i].Position.X -
                    ToastProjectiles[i].Width / 2, (int)ToastProjectiles[i].Position.Y -
                    ToastProjectiles[i].Height / 2, ToastProjectiles[i].Width, ToastProjectiles[i].Height);

                    rectangle2 = new Rectangle((int)player.Position.X,
                        (int)player.Position.Y,
                        player.Width,
                        player.Height);

                    // Determine if the two objects collided with each other
                    if (rectangle1.Intersects(rectangle2))
                    {
                        player.Health -= ToastProjectiles[i].Damage;
                        ToastProjectiles[i].Active = false;
                    }
                
            }
            for (int i = 0; i < sniperBullets.Count; i++)
            {

                // Create the rectangles we need to determine if we collided with each other
                rectangle1 = new Rectangle((int)sniperBullets[i].Position.X -
                sniperBullets[i].Width / 2, (int)sniperBullets[i].Position.Y -
                sniperBullets[i].Height / 2, sniperBullets[i].Width, sniperBullets[i].Height);

                rectangle2 = new Rectangle((int)player.Position.X,
                    (int)player.Position.Y,
                    player.Width,
                    player.Height);

                // Determine if the two objects collided with each other
                if (rectangle1.Intersects(rectangle2))
                {
                    if (MontageMode == true)
                    {
                        NoScope.Play();
                    }
                    player.Health -= sniperBullets[i].Damage;
                    sniperBullets[i].Active = false;
                }

            }
            for (int i = 0; i < bossToasterProjectiles.Count; i++) // Allowsx players to be hit by BOSS PROJECTILES TOASTER
            {
                //Allows players bullets to kill the toast
                // Create the rectangles we need to determine if we collided with each other
                rectangle1 = new Rectangle((int)bossToasterProjectiles[i].Position.X -
                bossToasterProjectiles[i].Width / 2, (int)bossToasterProjectiles[i].Position.Y -
                bossToasterProjectiles[i].Height / 2, bossToasterProjectiles[i].Width, bossToasterProjectiles[i].Height);

                if (rectangle1.Intersects(rectangle2))
                {
                    
                    player.Health -= bossToasterProjectiles[i].Damage;
                    bossToasterProjectiles[i].Active = false;
                }
            }
            for (int i = 0; i < pamBossProjectiles.Count; i++) // Allowsx players to be hit by Pam's Projectiles
            {
                
                // Create the rectangles we need to determine if we collided with each other
                rectangle1 = new Rectangle((int)pamBossProjectiles[i].Position.X -
                pamBossProjectiles[i].Width / 2, (int)pamBossProjectiles[i].Position.Y -
                pamBossProjectiles[i].Height / 2, pamBossProjectiles[i].Width, pamBossProjectiles[i].Height);

                if (rectangle1.Intersects(rectangle2))
                {

                    player.Health -= pamBossProjectiles[i].Damage;
                    pamBossProjectiles[i].Active = false;
                }
            }
        } // End Update


        private void PlayMusic(Song song)
        {
            // Due to the way the MediaPlayer plays music,
            // we have to catch the exception. Music will play when the game is not tethered
            
                try
                {
                    // Play the music
                    MediaPlayer.Play(song);
                    
                    // Loop the currently playing song
                    MediaPlayer.IsRepeating = true;
                }
                catch { }
            
        }
        
        
        private void UpdateProjectiles(GameTime gameTime)
        {
            // Update the Projectiles
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                projectiles[i].Update();

                if (projectiles[i].Active == false)
                {
                    projectiles.RemoveAt(i);
                }
            }
            
           for (int i = ToastProjectiles.Count - 1; i >= 0; i--) // Toast Projectiles
              {
                 ToastProjectiles[i].Update();

                 if (ToastProjectiles[i].Active == false)
                 {
                     ToastProjectiles.RemoveAt(i);
                 }
              }

           for (int i = sniperBullets.Count - 1; i >= 0; i--) // Toast Projectiles
           {
               sniperBullets[i].Update();

               if (sniperBullets[i].Active == false)
               {
                   sniperBullets.RemoveAt(i);
               }
           }
           for (int i = bossToasterProjectiles.Count - 1; i >= 0; i--) // Toast Projectiles
           {
               bossToasterProjectiles[i].Update(gameTime);

               if (bossToasterProjectiles[i].Active == false)
               {
                   bossToasterProjectiles.RemoveAt(i);
               }
           }
           for (int i = pamBossProjectiles.Count - 1; i >= 0; i--) // Toast Projectiles
           {
               pamBossProjectiles[i].Update(gameTime);

               if (pamBossProjectiles[i].Active == false)
               {
                   pamBossProjectiles.RemoveAt(i);
               }
           }
            //bossToasterProjectiles
            
        }// End Update
        public void Despawn()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Active = false;
            }
            for (int i = 0; i < toastEnemies.Count; i++)
            {
                toastEnemies[i].Active = false;
            }
            for (int i = 0; i < sniperEnemies.Count; i++)
            {
                sniperEnemies[i].Active = false;
            }
           
        }
        public void Death(GameTime gameTime)
        {
            PreviousPUSpawn = gameTime.TotalGameTime;
            previousSpawnTime = gameTime.TotalGameTime;
            toastPreviousSpawnTime = gameTime.TotalGameTime;
            sniperPreviousSpawnTime = gameTime.TotalGameTime;
            for (int i = ToastProjectiles.Count - 1; i >= 0; i--) // Toast Projectiles
            {
                ToastProjectiles.RemoveAt(i);
            }
            for (int i = sniperBullets.Count - 1; i >= 0; i--) // Toast Projectiles
            {
                sniperBullets.RemoveAt(i);
            }
            for (int i = bossToasterProjectiles.Count - 1; i >= 0; i--) // Toast Projectiles
            {
                bossToasterProjectiles.RemoveAt(i);
            }
            for (int i = pamBossProjectiles.Count - 1; i >= 0; i--) // Toast Projectiles
            {
                pamBossProjectiles.RemoveAt(i);
            }

        }
    }
    
}

