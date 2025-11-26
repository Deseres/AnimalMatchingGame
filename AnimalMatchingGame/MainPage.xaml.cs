namespace AnimalMatchingGame;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }
    private void PlayAgainButton_Clicked(object sender, EventArgs e)
    {
        AnimalButtons.IsVisible = true;
        PlayAgainButton.IsVisible = false;

        List<string> animalEmoji = new()
        {
            "🐯", "🐯",
            "🐺", "🐺",
            "🐵", "🐵",
            "🦒", "🦒",
            "🦝", "🦝",
            "🐮", "🐮",
            "🐷", "🐷",
            "🐰", "🐰",
        };

        foreach (var button in AnimalButtons.Children.OfType<Button>())
        {
            int index = Random.Shared.Next(animalEmoji.Count);
            string nextEmoji = animalEmoji[index];
            button.Text = nextEmoji;
            animalEmoji.RemoveAt(index);
        }
        Dispatcher.StartTimer(TimeSpan.FromSeconds(.1), TimerTick);
        
    }
    int tenthOfSecondsLeft = 100;
    int bestResult = 0;

    private bool TimerTick()
    {
        if (!this.IsLoaded) return false;

        tenthOfSecondsLeft--;

        TimeLeft.Text = "Time left: " + (tenthOfSecondsLeft / 10F).ToString("0.0s");

        if (PlayAgainButton.IsVisible)
        {
            tenthOfSecondsLeft = 100;
            return false;
        }
        if (tenthOfSecondsLeft == 0)
        {
            matchesFound = 0;
            AnimalButtons.IsVisible = false;
            PlayAgainButton.IsVisible = true;
        }
        return true;

        throw new NotImplementedException();
    }

    Button lastClicked;
    bool findingMatch = false;
    int matchesFound;

    private void AnimalButton_Clicked(object sender, EventArgs e)
    {
        if (sender is Button buttonClicked)
        {
            if (!string.IsNullOrEmpty(buttonClicked.Text) && (findingMatch == false))
            {
                buttonClicked.BackgroundColor = Colors.Red;
                lastClicked = buttonClicked;
                findingMatch = true;
            }
            else
            {
                if ((buttonClicked != lastClicked) && (buttonClicked.Text == lastClicked.Text)
                    && (!String.IsNullOrWhiteSpace(buttonClicked.Text)))
                {
                    matchesFound++;
                    lastClicked.Text = " ";
                    buttonClicked.Text = " ";
                }
                lastClicked.BackgroundColor = Colors.LightBlue;
                buttonClicked.BackgroundColor = Colors.LightBlue;
                findingMatch = false;
            }

            if (matchesFound == 8)
            {
                matchesFound = 0;
                AnimalButtons.IsVisible = false;
                PlayAgainButton.IsVisible = true;
                if (tenthOfSecondsLeft > bestResult)
                {
                    bestResult = tenthOfSecondsLeft;
                }
                BestResultLabel.Text = "Best result: " + (bestResult / 10F).ToString("0.0s");
            }
        }
    }
}
