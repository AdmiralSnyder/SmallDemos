using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp;

public enum RockPaperScissors
{
    Rock = 0,
    Paper = 1,
    Scissors = 2,
}

public enum GameResults
{
    Won = 1,
    Loose = 2,
    Draw = 3,
}

internal class RockPaperScissorsGame
{

    private int AddNumbers(int number1, int number2)
    {
        return number1 + number2;
    }


    public void Play()
    {
        AddNumbers(3, 4);

        Print("Welcome, please pick something");
        
        if (ChooseRockPaperScissors(out var choice))
        {
            RockPaperScissors compChoice = ComputerChooseRockPaperScissors();
            Print($"Computer chose {compChoice}");

            switch (Beats(choice, compChoice))
            {
                case GameResults.Won: Print("You won"); break;
                case GameResults.Loose: Print("You Lost"); break;
                case GameResults.Draw: Print("that's a draw."); break;
            }
        }
    }

    private void Print(string text) => Console.WriteLine(text);

    private GameResults Beats2(RockPaperScissors choice, RockPaperScissors compChoice) => (choice, compChoice) switch
    {
        (RockPaperScissors.Rock, RockPaperScissors.Rock) => GameResults.Draw,
        (RockPaperScissors.Rock, RockPaperScissors.Paper) => GameResults.Won,
        (RockPaperScissors.Rock, RockPaperScissors.Scissors) => GameResults.Loose,

        (RockPaperScissors.Paper, RockPaperScissors.Rock) => GameResults.Won,
        (RockPaperScissors.Paper, RockPaperScissors.Paper) => GameResults.Draw,
        (RockPaperScissors.Paper, RockPaperScissors.Scissors) => GameResults.Loose,

        (RockPaperScissors.Scissors, RockPaperScissors.Rock) => GameResults.Loose,
        (RockPaperScissors.Scissors, RockPaperScissors.Paper) => GameResults.Won,
        (RockPaperScissors.Scissors, RockPaperScissors.Scissors) => GameResults.Draw,
    };


    private GameResults Beats(RockPaperScissors choice, RockPaperScissors compChoice) => choice switch
    {
        // rock > scissors
        // paper > rock
        // ...
        RockPaperScissors.Rock => compChoice switch
        {
            RockPaperScissors.Rock => GameResults.Draw,
            RockPaperScissors.Paper => GameResults.Loose,
            RockPaperScissors.Scissors => GameResults.Won,
        },
        RockPaperScissors.Paper => compChoice switch
        {
            RockPaperScissors.Rock => GameResults.Won,
            RockPaperScissors.Paper => GameResults.Draw,
            RockPaperScissors.Scissors => GameResults.Loose,
        },
        RockPaperScissors.Scissors => compChoice switch
        {
            RockPaperScissors.Rock => GameResults.Loose,
            RockPaperScissors.Paper => GameResults.Won,
            RockPaperScissors.Scissors => GameResults.Draw,
        }
    };

    Random Random = new((int)(DateTime.Now.Ticks & (long)0xFFFFFF));

    /// <summary>
    /// Use a Random to pick one of R P S
    /// </summary>
    /// <returns></returns>
    private RockPaperScissors ComputerChooseRockPaperScissors() => 
        (RockPaperScissors)Random.Next((int)RockPaperScissors.Scissors);

    private bool ChooseRockPaperScissors(out RockPaperScissors result)
    {
        // show the options
        Print("type 'Rock', 'Paper' or 'Scissors' (or something else to exit...)");
        
        // ask user for input
        var res = Console.ReadLine().Trim().ToLowerInvariant();

        // evaluate
        var check = res switch
        {
            "rock" => (true, RockPaperScissors.Rock),
            "paper" => (true, RockPaperScissors.Paper),
            "scissors" => (true, RockPaperScissors.Scissors),
            _ => (false, RockPaperScissors.Rock),
        };
        result = check.Item2;
        return check.Item1;
    }

}
