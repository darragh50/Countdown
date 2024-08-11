using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Timers;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Countdown;

public partial class GamePage : ContentPage
{
    private char[] vowels = new char[] { 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'I', 'I', 'I', 'I', 'I', 'I', 'I', 'I', 'I', 'I', 'I', 'I', 'I', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'U', 'U', 'U', 'U', 'U' };//Vowel array list
    private char[] consonants = new char[] { 'B', 'B', 'C', 'C', 'C', 'D', 'D', 'D', 'D', 'D', 'D', 'F', 'F', 'G', 'G', 'G', 'H', 'H', 'J', 'K', 'L', 'L', 'L', 'L', 'L', 'M', 'M', 'M', 'M', 'N', 'N', 'N', 'N', 'N', 'N', 'N', 'N', 'P', 'P', 'P', 'P', 'Q', 'R', 'R', 'R', 'R', 'R', 'R', 'R', 'R', 'R', 'S', 'S', 'S', 'S', 'S', 'S', 'S', 'S', 'S', 'T', 'T', 'T', 'T', 'T', 'T', 'T', 'T', 'T', 'V', 'W', 'X', 'Y', 'Z' };//Consonant array list
    private List<char> selectedLetters = new List<char>();
    private System.Timers.Timer gameTimer;//Create timer
    private int timeLeft = 30;
    private int player1TotalScore = 0;
    private int player2TotalScore = 0;
    private int currentRound = 1;
    private const int totalRounds = 6;
    private Button submitWordsButton;
    private Entry Player1Word;
    private Entry Player2Word;
    private bool timerEnded = false;
    private bool isDictionaryLoaded = false;

    public GamePage()
    {
        InitializeComponent();
        CreateUI();
        InitializeSelectedLetters();
        Shuffle();
        this.BackgroundColor = Color.FromArgb(DefaultConstants.GetBackgroundColor());
        LoadDictionaryAsync();
        submitWordsButton.IsEnabled = false;
    }

    //Create design
    private void CreateUI()
        {
        var stackLayout = new StackLayout { Padding = 20 };

        Player1Word = new Entry
        {
            Placeholder = "Player 1 word",
            HorizontalOptions = LayoutOptions.Center,
            PlaceholderColor = Colors.LightGray,
            WidthRequest = 240,
            Margin = new Thickness(0, 0, 0, 10),
            FontSize = 16,
            IsEnabled = false
        };
        Player2Word = new Entry
        {
            Placeholder = "Player 2 word",
            HorizontalOptions = LayoutOptions.Center,
            PlaceholderColor = Colors.LightGray,
            WidthRequest = 240,
            Margin = new Thickness(0, 0, 0, 10),
            FontSize = 16,
            IsEnabled = false
        };

        Button consButton = new Button
        {
            Text = "Consonant",
            BackgroundColor = Color.FromArgb(DefaultConstants.GetButtonBackgroundColor(1)),
            TextColor = Colors.White,
            FontSize = 20,
            HeightRequest = 60,
            WidthRequest = 250,
            CornerRadius = 10,
            Margin = 5,
            HorizontalOptions = LayoutOptions.Center
        };

        Button vowelButton = new Button
        {
            Text = "Vowel",
            BackgroundColor = Color.FromArgb(DefaultConstants.GetButtonBackgroundColor(1)),
            TextColor = Colors.White,
            FontSize = 20,
            HeightRequest = 60,
            WidthRequest = 250,
            CornerRadius = 10,
            Margin = 5,
            HorizontalOptions = LayoutOptions.Center
        };

        Button timerButton = new Button
        {
            Text = "Start Timer",
            BackgroundColor = Color.FromArgb(DefaultConstants.GetButtonBackgroundColor(2)),
            TextColor = Colors.White,
            FontSize = 20,
            HeightRequest = 60,
            WidthRequest = 250,
            CornerRadius = 10,
            Margin = 5,
            HorizontalOptions = LayoutOptions.Center
        };

        submitWordsButton = new Button
        {
            Text = "Submit Words",
            BackgroundColor = Color.FromArgb(DefaultConstants.GetButtonBackgroundColor(2)),
            TextColor = Colors.White,
            FontSize = 20,
            HeightRequest = 60,
            WidthRequest = 250,
            CornerRadius = 10,
            Margin = 5,
            IsEnabled = false,
            HorizontalOptions = LayoutOptions.Center
        };

        //Assigns each function to button
        consButton.Clicked += OnConsonantBtnClicked;
        vowelButton.Clicked += OnVowelBtnClicked;
        timerButton.Clicked += OnStartTimerClicked;
        submitWordsButton.Clicked += OnSubmitWordsClicked;

        //Design of the page
        topLevel.Children.Add(Player1Word);
        topLevel.Children.Add(Player2Word);

        middleLevel1.Children.Add(consButton);
        middleLevel1.Children.Add(vowelButton);
        middleLevel2.Children.Add(timerButton);
        middleLevel2.Children.Add(submitWordsButton);
    }

    private async void LoadDictionaryAsync()
    {
        //Call the wordList.cs and loads it
        await WordList.LoadWordsAsync();
        isDictionaryLoaded = true;
    }

    private void Shuffle()
    {
        //Shuffle the vowels and consonants array
        Random.Shared.Shuffle(vowels);
        Random.Shared.Shuffle(consonants);
    }

    private void OnConsonantBtnClicked(object sender, EventArgs e)
    {
        if (selectedLetters.Count < 9)
        {
            //Randomly selects a consonant from the array, adds it and updates label
            char newLetter = consonants[new Random().Next(consonants.Length)];
            selectedLetters.Add(newLetter);
            UpdateLettersLabel();
        }
    }

    private void OnVowelBtnClicked(object sender, EventArgs e)
    {
        if (selectedLetters.Count < 9)
        {
            //Randomly selects a vowel from the array, adds it and updates label
            char newLetter = vowels[new Random().Next(vowels.Length)];
            selectedLetters.Add(newLetter);
            UpdateLettersLabel();
        }
    }

    //Joins player's letter entry together
    private void UpdateLettersLabel()
    {
        LetterGrid.Text = string.Join(" ", selectedLetters);
    }

    private void OnStartTimerClicked(object sender, EventArgs e)
    {
        if (timerEnded)
        {
            return; //Return nothing if timer has already ended
        }

        StartTimer();
    }

    private void StartTimer()
    {
        timeLeft = 30;
        TimerLabel.Text = $"Time left: {timeLeft}s";
        gameTimer = new System.Timers.Timer(1000);
        gameTimer.Elapsed += TimedEvent;
        gameTimer.Start();
        //Enable buttons and word entry is enabled once timer has started
        Player1Word.IsEnabled = true;
        Player2Word.IsEnabled = true; 
        submitWordsButton.IsEnabled = true;
        timerEnded = true;

    }

    private void TimedEvent(System.Object source, ElapsedEventArgs e)
    {
        if (timeLeft > 0)
        {
            timeLeft--;
            TimerLabel.Dispatcher.Dispatch(() => TimerLabel.Text = $"Time left: {timeLeft}s");
        }
        else
        {
            gameTimer.Stop();
            TimerLabel.Dispatcher.Dispatch(() => TimerLabel.Text = "Time's up!");
            Device.BeginInvokeOnMainThread(() => OnSubmitWordsClicked(null, null)); //Automatically submits words (regardless of entry) when timer is done
            timerEnded = true; //Make sure timer is ended

        }
    }

    //Function to make sure selected letters are lowercase
    private void InitializeSelectedLetters()
    {
        selectedLetters = selectedLetters.Select(char.ToLower).ToList();
    }

    private void OnSubmitWordsClicked(object sender, EventArgs e)
    {
        gameTimer.Stop();//Stop timer when submit button is clicked

        //Make sure all buttons are disabled until timer button is clicked again
        Player1Word.IsEnabled = false; 
        Player2Word.IsEnabled = false; 
        submitWordsButton.IsEnabled = false; 

        //Convert player submission to lowercase
        string player1Word = Player1Word.Text?.ToLower() ?? "";
        string player2Word = Player2Word.Text?.ToLower() ?? "";

        //Call validate function to check submission
        bool player1Valid = ValidateWord(player1Word);
        bool player2Valid = ValidateWord(player2Word);

        //Check length of submission
        int player1Score = player1Valid ? player1Word.Length : 0;
        int player2Score = player2Valid ? player2Word.Length : 0;

        //Score increase
        player1TotalScore += player1Score;
        player2TotalScore += player2Score;

        if (player1Score > player2Score)
        {
            ResultLabel.Text = $"Player 1 wins the round with {player1Word} ({player1Score} points)";
        }
        else if (player2Score > player1Score)
        {
            ResultLabel.Text = $"Player 2 wins the round with {player2Word} ({player2Score} points)";
        }
        else if (player1Score == player2Score && player1Score != 0)
        {
            ResultLabel.Text = $"It's a tie for this round with {player1Word} and {player2Word} ({player1Score} points each)";
        }
        else
        {
            ResultLabel.Text = "Player had invalid words or scored zero.";
        }

        currentRound++;
        if (currentRound > totalRounds)
        {
            EndGame();
        }
        else
        {
            StartNextRound();
        }
    }

    private bool ValidateWord(string word)
    {
        //Change all selected letters to lowercase
        var lowercaseSelectedLetters = selectedLetters.Select(char.ToLower).ToList();

        //Check if the dictionary has the word & the word entered is using the selected words
        return WordList.ContainsWord(word) && word.All(c => lowercaseSelectedLetters.Contains(c));
    }

    private void StartNextRound()
    {
        //Clear previous letters & update labels
        selectedLetters.Clear();
        UpdateLettersLabel();
        UpdateCurrentRoundLabel();
        //Make sure all buttons are disabled - and strings are empty - until timer button is clicked again
        Player1Word.Text = string.Empty;
        Player2Word.Text = string.Empty;
        Player1Word.IsEnabled = false; 
        Player2Word.IsEnabled = false;
        submitWordsButton.IsEnabled = false;
        InitializeSelectedLetters();
        timerEnded = false;
    }
    
    //Update the UI
    private void UpdateCurrentRoundLabel()
    {
        CurrentRoundLabel.Text = $"Round {currentRound} of {totalRounds}";
    }

    private void EndGame()
    {
        string finalResult;

        if (player1TotalScore > player2TotalScore)
        {
            finalResult = $"Player 1 wins the game with {player1TotalScore} points!";
        }
        else if (player2TotalScore > player1TotalScore)
        {
            finalResult = $"Player 2 wins the game with {player2TotalScore} points!";
        }
        else
        {
            finalResult = "The game ends in a tie!";
        }

        ResultLabel.Text = finalResult;
        RestartButton.IsVisible = true;
        // Disable all input fields until timer starts again
        Player1Word.IsEnabled = false; 
        Player2Word.IsEnabled = false;
        submitWordsButton.IsEnabled = false;
    }

    private void OnRestartGameClicked(object sender, EventArgs e)
    {
        //Reset all values, labels and button choices
        player1TotalScore = 0;
        player2TotalScore = 0;
        currentRound = 1;
        selectedLetters.Clear();
        UpdateLettersLabel();
        Player1Word.Text = string.Empty;
        Player2Word.Text = string.Empty;
        TimerLabel.Text = "Start the timer for the next round";
        ResultLabel.Text = string.Empty;
        RestartButton.IsVisible = false;
        UpdateCurrentRoundLabel();
        timeLeft = 30;
        timerEnded = false;
    }
}
