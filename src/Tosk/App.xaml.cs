using Tosk.Commons.SQLite;

namespace Tosk
{
    public partial class App : Application
    {
        private readonly BaseDbContext _dbContext;
        public App(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
            InitializeComponent();
            MainPage = new MainPage();
        }

        protected override async void OnStart()
        {
            base.OnStart();
            await _dbContext.Initialize();
        }
    }
}
