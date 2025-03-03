using System;


namespace CaveJourner{

    static class GlobalStore{
        public static ConsoleKeyInfo keyPress;
        public static string selectedColour = "\u001b[32m";
        public static string escapeColour = "\u001b[0m";
        public static int score = 0;
        public static Boolean playing = true;
        public static TimeSpan sessionTime;
        public static int location = 0;
    }



    class Display{

        //only works on windows
        // public static void InitialiseDisplay(){
        //     Console.SetWindowSize(40, 40);
        // }

        public void WaitAnimation(){

            for (int i =0; i<3; i++){
                Console.Write("                          ...                        \n");
                System.Threading.Thread.Sleep(1000); 
            }  
        }

        public void StartDisplay(){
            Console.WriteLine("(====================================================)");
            Console.WriteLine("|                                                    |");
            Console.WriteLine("|                   cave journer                     |");
            Console.WriteLine("|                                                    |");
            Console.WriteLine("(====================================================)");
        }

        public void Intro(){
            Console.WriteLine("(====================================================)");
            //instructiuons for game here
            Console.WriteLine("(====================================================)");
        }

        public void Paths(){
            //class can be its own class when there are multipl

            Console.WriteLine(@"(====================================================)");
            Console.WriteLine(@"|       ###########           ##############         |");
            Console.WriteLine(@"|         €€€€€€€                &&&&&&&             |");
            Console.WriteLine(@"|           £££                    %%%               |");
            Console.WriteLine(@"|             |                     |                |");
            Console.WriteLine(@"|             |                     }                |");
            Console.WriteLine(@"|^^^}         |       {^*^^^^}      |            ^^^^|");
            Console.WriteLine(@"|    )|       |      |        }     |          (     |");
            Console.WriteLine(@"|     )}@     |     {(        )]    |         {      |");
            Console.WriteLine(@"|     )|      |      {        }     |         (      |");
            Console.WriteLine(@"| ___|        |       (      )      |          {_____|");
            Console.WriteLine(@"|&&&|         |       @@@@@&&&      |        #######@|");
            Console.WriteLine(@"|&&&%%|       |     £££££####$$$    |      |@@@@@&&&&|");
            Console.WriteLine(@"|      \      |    /             \  |     /         /|");
            Console.WriteLine(@"|           \| /                 \ |/               /|");
            Console.WriteLine(@"|            |                  *                  /#|");
            Console.WriteLine(@"|!\                                                /#|"); 
            Console.WriteLine(@"|}\                     *                          /#|");
            Console.WriteLine(@"(====================================================)");
    

            //ascii art of the paths ...
        }


        public void ChoosingSelection(string option1, string option2, string option3, int currentSelected){
            Boolean choosing = true;
            string[] options = [option1, option2, option3];
            (int x, int y) = Console.GetCursorPosition();

            while(choosing){
                Console.SetCursorPosition(x, y);

                Console.Write("\t");

                for (int i = 0; i < options.Length; i++){
                    if (currentSelected == i){
                        Console.Write(GlobalStore.selectedColour + options[i] + GlobalStore.escapeColour + "\t\t");
                    }
                    else Console.Write(options[i] + GlobalStore.escapeColour + "\t\t");
                }

                GlobalStore.keyPress = Console.ReadKey(true);

                switch (GlobalStore.keyPress.Key){
                    case ConsoleKey.RightArrow:
                        currentSelected++;
                        if(currentSelected > 2){
                            currentSelected = 0;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        currentSelected--;
                        if(currentSelected < 0){
                            currentSelected = 2;
                        }
                        break;
                    case ConsoleKey.Enter:
                        choosing = false;
                        break;
                }

            }

         
    }

 

    class Game{
        public void MoveLocation(){}



        public void EndGame(){
            GlobalStore.playing = false;
        }


    }

    class Timer{
        public static TimeSpan getSessionTime(){
            return GlobalStore.sessionTime;
        }

        public static TimeSpan setSessionTime(TimeSpan currentSessionTime){
            GlobalStore.sessionTime += currentSessionTime;

            return GlobalStore.sessionTime;
        }

        public static DateTime getTime(){
            return DateTime.Now;
        }
        public static TimeSpan timeDiffT(DateTime start, DateTime end){    
            return end.Subtract(start);
        }

        public static void WaitSeconds(int seconds){
            int milisecs = seconds * 1000;

            System.Threading.Thread.Sleep(milisecs); 
        }

    }

    class Quiz{
        string[] questions = {"How was your weekend?", "What's your best talent?", "Who is your icon?"};
        string[,] answers = {{"awful...", "decent", "the best"}, {"My eloquence", "My athleticism", "My candour"}, {"Ru-Paul", "Myself", "Nobody"}};
        int[] correctAns = [2, 0, 0];
        int choices = 3;

        public void DisplayScore(){
            Console.WriteLine($"You've earned a score of {GlobalStore.score}");
        }

        public void DisplayQuestion(int question){
            Console.WriteLine(questions[question]);
        }
    

        public void DisplayAnswers(int question){
            for (int ans = 0; ans < 3; ans++){
                    Console.WriteLine(answers[question, ans]);
                }
        }

        private void Score(int question, int selected){
            if(selected == correctAns[question]){
                    GlobalStore.score++;
                    Console.WriteLine("Score + 1");
            }
            else{            
                Console.WriteLine("");
            }
        }

        public void DisplayAnswersWithUserInput(int question){
            int selectedOption = 0;
            Boolean thinkingOfAnswer = true;
            //after asking the question, get the cursor position
            (int x, int y) = Console.GetCursorPosition();


            while (thinkingOfAnswer){
                Console.SetCursorPosition(x, y);
                for (int i = 0; i < choices; i++){
                    if (selectedOption == i){
                        Console.WriteLine(GlobalStore.selectedColour + answers[question, i] + GlobalStore.escapeColour);
                    }
                    else Console.WriteLine(answers[question, i] + GlobalStore.escapeColour);
                }

                GlobalStore.keyPress = Console.ReadKey(true);

                switch (GlobalStore.keyPress.Key){
                    case ConsoleKey.DownArrow:
                        selectedOption++;
                        if(selectedOption > 2){
                            selectedOption = 0;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        selectedOption--;
                        if(selectedOption < 0){
                            selectedOption = 2;
                        }
                        break;
                    case ConsoleKey.Enter:
                        thinkingOfAnswer = false;
                        break;
                }
            } 

            Score(question, selectedOption);

            // if(selectedOption == correctAns[question]){
            //         GlobalStore.score++;
            // }

        }
    }

    class Program{

        static void Main(string[] args){
            DateTime startTime = Timer.getTime();

            Display gameDisplay = new Display();
            Game game = new Game();

            while (GlobalStore.playing){
                gameDisplay.StartDisplay();

                //gameDisplay.WaitAnimation();
                //Timer.WaitSeconds(3);

                gameDisplay.Paths();

                gameDisplay.ChoosingSelection("left", "forwards", "right", 0);

                game.EndGame();
            }
 

            
            DateTime endTime = Timer.getTime();

            //time lasted and sessiontime are literally the sam elol but im used dto useing store?? which better
            TimeSpan timeLasted = Timer.timeDiffT(startTime, endTime);
            GlobalStore.sessionTime = Timer.setSessionTime(timeLasted);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nTime Played: {Timer.getSessionTime().ToString(@"mm\:ss\.ff")}  ...");




            Console.ResetColor();       

            // while (startQuiz){
            //     for (int quizQuestion = 0; quizQuestion < 3; quizQuestion ++){
            //         myQuiz.DisplayQuestion(quizQuestion);

            //         myQuiz.DisplayAnswersWithUserInput(quizQuestion);

            //     }
            //     startQuiz = false;
            // }

            // myQuiz.DisplayScore();



        }

    }
}}



