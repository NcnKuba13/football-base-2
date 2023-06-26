﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace MVVM_Football_Informant.ViewModel
{
    using MVVM_Football_Informant.Model;
    using BaseClass;
    using DAL.Entities;

    class GameViewModel : ViewModelBase
    {
        #region Private Fields
        private Model model = null;

        private string actualTarget = null;
        private string actualType = null;

        private List<string> contentOfTargetsComboBox = new List<string>();
        private List<string> contentOfTypesComboBox = new List<string>();

        private List<string> typesToClubs = new List<string>();
        private List<string> typesToStadiums = new List<string>();

        private bool menuPanelIsEnable = true;
        private Visibility menuPanelButtonsVisibility = Visibility.Visible;

        private Visibility typesOfGameVisibility = Visibility.Hidden;
        private Visibility startButtonVisibility = Visibility.Hidden;
        private Visibility gamesVisibility = Visibility.Visible;
        private Visibility gamesWorkedVisibility = Visibility.Hidden;
        private Visibility gamesEndVisibility = Visibility.Hidden;
        private Visibility nEXTButtonVisibility = Visibility.Hidden;

        private int numberOfRounds = 5;
        private int roundNumber = 0;

        private ObservableCollection<Club> clubs = null;
        private ObservableCollection<Stadium> stadiums = null;

        Random rnd = new Random();

        private List<dynamic> pairsToQuiz = new List<dynamic>();

        private string decisionElement1 = null;
        private string decisionElement2 = "=";
        private string decisionElement3 = null;
        private string finalDecisionElement = null;

        private int numberOfCorrectAnswer = 0;

        private bool endGame = false;
        private string gameQuestion = null;
        #endregion

        #region Constructors
        public GameViewModel(Model model)
        {
            this.model = model;
            this.clubs = model.Clubs;
            this.stadiums = model.Stadiums;

            contentOfTargetsComboBox.Add("Kluby");
            contentOfTargetsComboBox.Add("Stadiony");

            typesToClubs.Add("Trofea");
            typesToClubs.Add("Wartość");

            typesToStadiums.Add("Pojemność");
        }
        #endregion

        #region Properties
        public string ActualTarget
        {
            get { return actualTarget; }
            set
            {
                actualTarget = value;
                onPropertyChanged(nameof(ActualTarget));
            }
        }

        public string ActualType
        {
            get { return actualType; }
            set
            {
                actualType = value;
                onPropertyChanged(nameof(ActualType));
            }
        }

        public int NumberOfRounds
        {
            get { return numberOfRounds; }
            set
            {
                numberOfRounds = value;
                onPropertyChanged(nameof(NumberOfRounds));
            }
        }

        public bool MenuPanelIsEnable
        {
            get { return menuPanelIsEnable; }
            set
            {
                menuPanelIsEnable = value;
                onPropertyChanged(nameof(MenuPanelIsEnable));
            }
        }

        public Visibility MenuPanelButtonsVisibility
        {
            get { return menuPanelButtonsVisibility; }
            set
            {
                menuPanelButtonsVisibility = value;
                onPropertyChanged(nameof(MenuPanelButtonsVisibility));
            }
        }

        public Visibility TypesOfGameVisibility
        {
            get { return typesOfGameVisibility; }
            set
            {
                typesOfGameVisibility = value;
                onPropertyChanged(nameof(TypesOfGameVisibility));
            }
        }
        
        public Visibility StartButtonVisibility
        {
            get { return startButtonVisibility; }
            set
            {
                startButtonVisibility = value;
                onPropertyChanged(nameof(StartButtonVisibility));
            }
        }

        public Visibility GamesVisibility
        {
            get { return gamesVisibility; }
            set
            {
                gamesVisibility = value;
                onPropertyChanged(nameof(GamesVisibility));
            }
        }

        public Visibility GamesWorkedVisibility
        {
            get { return gamesWorkedVisibility; }
            set
            {
                gamesWorkedVisibility = value;
                onPropertyChanged(nameof(GamesWorkedVisibility));
            }
        }

        public Visibility GamesEndVisibility
        {
            get { return gamesEndVisibility; }
            set
            {
                gamesEndVisibility = value;
                onPropertyChanged(nameof(GamesEndVisibility));
            }
        }

        public Visibility NEXTButtonVisibility
        {
            get { return nEXTButtonVisibility; }
            set
            {
                nEXTButtonVisibility = value;
                onPropertyChanged(nameof(NEXTButtonVisibility));
            }
        }

        public List<string> ContentOfTargetsComboBox
        {
            get { return contentOfTargetsComboBox; }
            set
            {
                contentOfTargetsComboBox = value;
                onPropertyChanged(nameof(ContentOfTargetsComboBox));
            }
        }

        public List<string> ContentOfTypesComboBox
        {
            get { return contentOfTypesComboBox; }
            set
            {
                contentOfTypesComboBox = value;
                onPropertyChanged(nameof(ContentOfTypesComboBox));
            }
        }

        public string DecisionElement1
        {
            get { return decisionElement1; }
            set
            {
                decisionElement1 = value;
                onPropertyChanged(nameof(DecisionElement1));
            }
        }

        public string DecisionElement2
        {
            get { return decisionElement2; }
        }

        public string DecisionElement3
        {
            get { return decisionElement3; }
            set
            {
                decisionElement3 = value;
                onPropertyChanged(nameof(DecisionElement3));
            }
        }

        public string FinalDecisionElement
        {
            get { return finalDecisionElement; }
            set
            {
                finalDecisionElement = value;
                onPropertyChanged(nameof(FinalDecisionElement));
            }
        }

        public int NumberOfCorrectAnswer
        {
            get { return numberOfCorrectAnswer; }
            set
            {
                numberOfCorrectAnswer = value;
                onPropertyChanged(nameof(NumberOfCorrectAnswer));
            }
        }

        public int RoundNumber
        {
            get { return roundNumber; }
            set
            {
                roundNumber = value;
                onPropertyChanged(nameof(RoundNumber));
            }
        }

        public string GameQuestion
        {
            get { return gameQuestion; }
            set
            {
                gameQuestion = value;
                onPropertyChanged(nameof(GameQuestion));
            }
        }
        #endregion

        #region Methods
        public List<dynamic> makeQuiz()
        {
            endGame = false;
            RoundNumber = 0;
            DecisionElement1 = null;
            DecisionElement3 = null;
            NumberOfCorrectAnswer = 0;

            var pairs = new List<dynamic>();

            if (ActualTarget.Equals("Kluby"))
            {
                if (ActualType.Equals("Trofea"))
                {
                    GameQuestion = "Klub, który ma więcej trofeów!";
                }
                else if (ActualType.Equals("Wartość"))
                {
                    GameQuestion = "Drużynę, który ma większą wartość!";
                }
            }
            else if (ActualTarget.Equals("Stadiony"))
            {
                if (ActualType.Equals("Pojemność"))
                {
                    GameQuestion = "Stadion, o większej pojemności!";
                }
            }

            while (pairs.Count < NumberOfRounds * 2)
            {
                if (ActualTarget.Equals("Kluby"))
                {
                    var elem1 = rnd.Next(clubs.Count - 1);
                    if (pairs.IndexOf(clubs[elem1]) == -1)
                    {
                        pairs.Add(clubs[elem1]);
                    }

                }
                else if (ActualTarget.Equals("Stadiony"))
                {
                    var elem1 = rnd.Next(stadiums.Count - 1);
                    if (pairs.IndexOf(stadiums[elem1]) == -1)
                    {
                        pairs.Add(stadiums[elem1]);
                    }
                }
            }

            return pairs;
        }

        public void loadNextPair()
        {
            if (RoundNumber >= NumberOfRounds)
            {
                endGame = true;
            }

            if(!endGame)
            { 
                if (ActualTarget.Equals("Kluby"))
                {
                    Club club = pairsToQuiz[RoundNumber * 2];
                    DecisionElement1 = club.Name;

                    club = pairsToQuiz[RoundNumber * 2 + 1];
                    DecisionElement3 = club.Name;
                }
                else if (ActualTarget.Equals("Stadiony"))
                {
                    Stadium stadium = pairsToQuiz[RoundNumber * 2];
                    DecisionElement1 = stadium.Name;

                    stadium = pairsToQuiz[RoundNumber * 2 + 1];
                    DecisionElement3 = stadium.Name;
                }
            }
            else
            {
                showResults();
            }
        }

        public void loadAnswer(object x)
        {
            FinalDecisionElement = x.ToString();
        }

        public void checkAnswer()
        {
            if (!endGame)
            {
                double firstToCompare = -1;
                double secondToCompare = -1;

                if (ActualTarget.Equals("Kluby"))
                {
                    Club club1 = pairsToQuiz[RoundNumber * 2];
                    Club club2 = pairsToQuiz[RoundNumber * 2 + 1];

                    if (ActualType.Equals("Trofea"))
                    {
                        firstToCompare = club1.NumberOfTrophies;
                        secondToCompare = club2.NumberOfTrophies;
                    }
                    else if (ActualType.Equals("Wartość"))
                    {
                        firstToCompare = club1.ValueOfTeam;
                        secondToCompare = club2.ValueOfTeam;
                    }

                    if ((firstToCompare == secondToCompare) && FinalDecisionElement.Equals("="))
                    {
                        NumberOfCorrectAnswer += 1;
                    }
                    else if ((firstToCompare > secondToCompare) && FinalDecisionElement.Equals(club1.Name))
                    {
                        NumberOfCorrectAnswer += 1;
                    }
                    else if ((firstToCompare < secondToCompare) && FinalDecisionElement.Equals(club2.Name))
                    {
                        NumberOfCorrectAnswer += 1;
                    }
                }
                else if (ActualTarget.Equals("Stadiony"))
                {
                    Stadium stadium1 = pairsToQuiz[RoundNumber * 2];
                    Stadium stadium2 = pairsToQuiz[RoundNumber * 2 + 1];

                    if (ActualType.Equals("Pojemność"))
                    {
                        firstToCompare = stadium1.Capacity;
                        secondToCompare = stadium2.Capacity;
                    }

                    if ((firstToCompare == secondToCompare) && FinalDecisionElement.Equals("="))
                    {
                        NumberOfCorrectAnswer += 1;
                    }
                    else if ((firstToCompare > secondToCompare) && FinalDecisionElement.Equals(stadium1.Name))
                    {
                        NumberOfCorrectAnswer += 1;
                    }
                    else if ((firstToCompare < secondToCompare) && FinalDecisionElement.Equals(stadium2.Name))
                    {
                        NumberOfCorrectAnswer += 1;
                    }
                }

                RoundNumber += 1;
            }
        }

        public void showResults()
        {
            GamesWorkedVisibility = Visibility.Hidden;

            GamesEndVisibility = Visibility.Visible;
        }
        #endregion

        #region ICommand
        private ICommand _loadTypesAndShowTypesComboBox = null;
        public ICommand LoadTypesAndShowTypesComboBox
        {
            get
            {
                if (_loadTypesAndShowTypesComboBox == null)
                    _loadTypesAndShowTypesComboBox = new RelayCommand(
                        arg => {
                            TypesOfGameVisibility = Visibility.Visible;

                            ActualType = null;

                            if (ActualTarget != null)
                            {
                                if (actualTarget.Equals(ContentOfTargetsComboBox[0]))
                                    ContentOfTypesComboBox = typesToClubs;
                                else if (actualTarget.Equals(ContentOfTargetsComboBox[1]))
                                    ContentOfTypesComboBox = typesToStadiums;
                            }

                            StartButtonVisibility = Visibility.Hidden;
                        },
                        arg => true
                        );

                return _loadTypesAndShowTypesComboBox;
            }
        }

        private ICommand _showStartButton = null;
        public ICommand ShowStartButton
        {
            get
            {
                if (_showStartButton == null)
                    _showStartButton = new RelayCommand(
                        arg => {
                            StartButtonVisibility = Visibility.Visible;
                            MenuPanelIsEnable = false;
                        },
                        arg => true
                        );

                return _showStartButton;
            }
        }

        private ICommand _quizStart = null;
        public ICommand QuizStart
        {
            get
            {
                if (_quizStart == null)
                    _quizStart = new RelayCommand(
                        arg => {
                            MenuPanelIsEnable = false;

                            MenuPanelButtonsVisibility = Visibility.Hidden;

                            GamesVisibility = Visibility.Hidden;
                            GamesWorkedVisibility = Visibility.Visible;

                            pairsToQuiz = makeQuiz();

                            loadNextPair();
                        },
                        arg => true
                        );

                return _quizStart;
            }
        }
        
        private ICommand _loadAnswer = null;
        public ICommand LoadAnswer
        {
            get
            {
                if (_loadAnswer == null)
                    _loadAnswer = new RelayCommand(
                            (x) => {
                                loadAnswer(x);

                                NEXTButtonVisibility = Visibility.Visible;
                            },
                            arg => true
                        );
                return _loadAnswer;
            }
        }

        private ICommand _checkAnswerAndLoadNextPair = null;
        public ICommand CheckAnswerAndLoadNextPair
        {
            get
            {
                if (_checkAnswerAndLoadNextPair == null)
                    _checkAnswerAndLoadNextPair = new RelayCommand(
                            arg => {
                                NEXTButtonVisibility = Visibility.Hidden;

                                checkAnswer();

                                loadNextPair();
                            },
                            arg => true
                        );
                return _checkAnswerAndLoadNextPair;
            }
        }

        private ICommand _quizKoniec = null;
        public ICommand QuizKoniec
        {
            get
            {
                if (_quizKoniec == null)
                    _quizKoniec = new RelayCommand(
                            arg => {
                                GamesVisibility = Visibility.Visible;

                                GamesEndVisibility = Visibility.Hidden;

                                ActualTarget = null;
                                ActualType = null;
                                NumberOfRounds = 5;

                                TypesOfGameVisibility = Visibility.Hidden;
                            },
                            arg => true
                        );
                return _quizKoniec;
            }
        }
        #endregion
    }
}
