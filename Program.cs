using System;


namespace CaveJourner{

    static class GlobalStore{
        public static ConsoleKeyInfo keyPress;
        public static string selectedColour = "\u001b[32m";
        public static string escapeColour = "\u001b[0m";
        public static int score = 0;
        public static Boolean playing = true;
       //public static TimeSpan sessionTime;
    }



    class Display{
        //only works on windows
        // public static void InitialiseDisplay(){
        //     Console.SetWindowSize(40, 40);
        // }

         public void Flash(){
            //class can be its own class when there are multipl

            Console.WriteLine(@"(====================================================)");
            Console.WriteLine(@"|                                                    |");
            Console.WriteLine(@"|                                                    |");
            Console.WriteLine(@"|         #                                    {}    |");
            Console.WriteLine(@"|                                                    |");
            Console.WriteLine(@"|                                                    |");
            Console.WriteLine(@"|                                  *                 |");
            Console.WriteLine(@"|                                                    |");
            Console.WriteLine(@"|                                                    |");
            Console.WriteLine(@"|                     :                              |");
            Console.WriteLine(@"|   #                                                |");
            Console.WriteLine(@"|                                                    |");
            Console.WriteLine(@"|                                                    |");
            Console.WriteLine(@"|                                                  ? |");
            Console.WriteLine(@"|                                                    |");
            Console.WriteLine(@"|            |                  *                  /#|");
            Console.WriteLine(@"|!\                                                /#|"); 
            Console.WriteLine(@"|}\                     *                          /#|");
            Console.WriteLine(@"(====================================================)");
        }

        public void WaitAnimation(){

            for (int i =0; i<3; i++){
                Console.Write("                          ...                        \n");
                System.Threading.Thread.Sleep(200); 
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

        public void setTextColour(ConsoleColor Colour){
             Console.ForegroundColor = Colour;

        }  
    }

    class Room{
        private static int roomNum = 0;
        private string description;
        private string itemName = "Nothing";

        public Room(int RoomNum, string Desc){
            roomNum = RoomNum;
            description = Desc;
        }

        public string getDescription(){
            return ($"You are standing {description}");
        }

    }

    class Player{
        private string name;
        private static int playerLocation = 0;
        private static int energy = 4;

        public Player(string Name){
            name = Name;
        }

        public string getName(){
            return name;
        }

        // public string setName(string PlayerName){
        //     //user sets their own name
        // }

        public int getLocation(){
            return playerLocation;
        }

        public void setLocation(int Loc){
            playerLocation = Loc;
        }
    }
 

    class Game{
        public string[] directions = {"left", "forwards", "right"};

        public void ChooseDirection(string option1, string option2, string option3, int currentSelected){
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
                        Console.Write("\n");
                        choosing = false;
                        break;
                }

            }
        }

        // public void DescribeLocation(int room){
        //     Console.WriteLine($"{Room.description[room]}");
        // }     

        public void EndGame(){
            GlobalStore.playing = false;
        }

    }

    class Timer{
        private static TimeSpan sessionTime;

        public static TimeSpan getSessionTime(){
            return sessionTime;
        }

        public static TimeSpan setSessionTime(TimeSpan currentSessionTime){
            sessionTime += currentSessionTime;

            return sessionTime;
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



            //user input to get players name before game?
            Player player = new Player("MyName");
            List<Room> rooms = new List<Room>();

            rooms.Add(new Room(0, "in a dimly-lit, cavernous space. \nAhead of you are 3 passages."));
            rooms.Add(new Room(1, "at an exposed cliff face.\n A sheer drop lies only a metre ahead."));

            while (GlobalStore.playing){

                //make some resetting display Console Location methods so that new text always appears after old

                    gameDisplay.StartDisplay();

                    gameDisplay.WaitAnimation();

                    gameDisplay.Paths();

                    string roomDescriptor = rooms[player.getLocation()].getDescription();
                    Console.WriteLine(roomDescriptor);

                    game.ChooseDirection("left", "forwards", "right", 0);

                    gameDisplay.Flash();

                    Timer.WaitSeconds(2);

                    game.EndGame();
            }
 

            
            DateTime endTime = Timer.getTime();
            TimeSpan timeLasted = Timer.timeDiffT(startTime, endTime);
            Timer.setSessionTime(timeLasted);


            gameDisplay.setTextColour(ConsoleColor.Cyan);
            Console.WriteLine($"\nTime Played: {Timer.getSessionTime().ToString(@"mm\:ss\.ff")}  ...");

            Console.ResetColor();       

        }

    }}




