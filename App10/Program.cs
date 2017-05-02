using System;
using System.Windows.Forms;
using System.Drawing;

class Question
{
    // Instance Members (become part of the object)
    private string statement, op1, op2, op3, op4;
    private int answer, marks;

    private int userChoice;

    public int UserChoice
    {
        get { return this.userChoice; }
        set
        {
            if (value >= 1 && value <= 4)
                this.userChoice = value;
        }
    }

    public Question(/* Question this = ref. of calling object */ string statement, string op1, string op2, string op3, string op4, int answer, int marks)
    {
        this.statement = statement;
        this.op1 = op1;
        this.op2 = op2;
        this.op3 = op3;
        this.op4 = op4;
        this.answer = answer;
        this.marks = marks;
    }

    public string Statement
    {
        get // string get_Statement(Question this) { return this.statement ; } 
        {
            return this.statement;
        }
    }

    public string Op1
    {
        get
        {
            return this.op1;
        }
    }

    public string Op2
    {
        get
        {
            return this.op2;
        }
    }

    public string Op3
    {
        get
        {
            return this.op3;
        }
    }

    public string Op4
    {
        get
        {
            return this.op4;
        }
    }

    public int Answer
    {
        get
        {
            return this.answer;
        }
    }
    public int Marks
    {
        get
        {
            return this.marks;
        }
    }
}

// Purpose: To get the data by preparing it with hard code values
class HardCodedQuestionsRespository
{
    public Question[] GetQuestions()
    {
        // this creates only array of reference variables
        Question[] questions = new Question[5];

        // this creates objects and intializes reference variables
        questions[0] = new Question("AAA", "A1", "A2", "A3", "A4", 1, 5);
        questions[1] = new Question("BBB", "B1", "B2", "B3", "B4", 2, 6);
        questions[2] = new Question("CCC", "C1", "C2", "C3", "C4", 3, 7);
        questions[3] = new Question("DDD", "D1", "D2", "D3", "D4", 4, 8);
        questions[4] = new Question("EEE", "E1", "E2", "E3", "E4", 1, 9);

        return questions;
    }
}

class DBRepository
{

}

class XMLRepository
{

}

class WebServiceRepository
{

}

class TestLogic
{
    Question[] questions; // instance data member 
    int index = -1; // instance data member
    int userMarks = 0; // instance data member

    public TestLogic()
    {
        HardCodedQuestionsRespository hr = new HardCodedQuestionsRespository();
        this.questions = hr.GetQuestions();
    }

    public Question GetNextQuestion()
    {
        if (this.index < this.questions.Length - 1)
            return this.questions[++this.index];
        else
            return null; // no questions left
    }

    public Question GetPreviousQuestion()
    {
        if (this.index > 0)
            return this.questions[--this.index];
        else
            return null; // no previous question
    }

    public int GetTotalMarks()
    {
        int totalMarks = 0;
        for (int i = 0; i < this.questions.Length; i++)
        {
            totalMarks += this.questions[i].Marks;
        }
        return totalMarks;
    }

    public void CalculateUserMarks(int choice)
    {
        this.questions[this.index].UserChoice = choice;

        // calculate user marks if the answer is correct
        //if (choice == this.questions[this.index - 1].Answer) // user answer is correct
        //{
        //    this.userMarks += this.questions[this.index - 1].Marks;
        //}
    }

    public int GetUserMarks()
    {
        for (int i = 0; i < questions.Length; i++)
        {
            if (questions[i].Answer == questions[i].UserChoice)
                userMarks += questions[i].Marks;
        }
        return this.userMarks;
    }
}


class GUI : Form
{
    TestLogic tl = new TestLogic(); // data member

    // UI control objects
    Label statement = new Label();
    RadioButton op1 = new RadioButton();
    RadioButton op2 = new RadioButton();
    RadioButton op3 = new RadioButton();
    RadioButton op4 = new RadioButton();
    Button next = new Button();
    Button previous = new Button();

    public GUI()
    {
        // make UI controls part of the window
        this.Controls.Add(statement);
        this.Controls.Add(op1);
        this.Controls.Add(op2);
        this.Controls.Add(op3);
        this.Controls.Add(op4);
        this.Controls.Add(next);
        this.Controls.Add(previous);

        statement.Location = new Point(10, 10);
        op1.Location = new Point(10, 50);
        op2.Location = new Point(10, 90);
        op3.Location = new Point(10, 130);
        op4.Location = new Point(10, 170);
        next.Location = new Point(10, 210);
        previous.Location = new Point(100, 210);

        next.Text = "Next";
        previous.Text = "Previous";

        next.Click += NextButtonClicked; // registering the event handler
                                         // so that the event handler gets called when button is clicked

        previous.Click += PreviousButtonClicked; // registering the event handler
        // so that the event handler gets called when button is clicked

        Question question = tl.GetNextQuestion();
        DisplayQuestion(question);
    }

    void PreviousButtonClicked(object sender, EventArgs e)
    {
        if (op1.Checked)
            tl.CalculateUserMarks(1);
        else if (op2.Checked)
            tl.CalculateUserMarks(2);
        else if (op3.Checked)
            tl.CalculateUserMarks(3);
        else if (op4.Checked)
            tl.CalculateUserMarks(4);

        Question question = tl.GetPreviousQuestion();
        if (question != null)
            DisplayQuestion(question);
    }

    // Button Click Event Handler
    void NextButtonClicked(object sender, EventArgs e)
    {
        if (op1.Checked)
            tl.CalculateUserMarks(1);
        else if (op2.Checked)
            tl.CalculateUserMarks(2);
        else if (op3.Checked)
            tl.CalculateUserMarks(3);
        else if (op4.Checked)
            tl.CalculateUserMarks(4);

        Question question = tl.GetNextQuestion();
        if (question == null)
        {
            MessageBox.Show("You obtained " + tl.GetUserMarks() + " out of " + tl.GetTotalMarks());

            this.Close(); // On closing the Window, Message Loop terminates
        }
        else
            DisplayQuestion(question);
    }

    private void DisplayQuestion(Question question)
    {
        switch (question.UserChoice)
        {
            case 1:
                op1.Checked = true;
                break;
            case 2:
                op2.Checked = true;
                break;
            case 3:
                op3.Checked = true;
                break;
            case 4:
                op4.Checked = true;
                break;
            default:
                op1.Checked = op2.Checked = op3.Checked = op4.Checked = false;
                break;
        }

        statement.Text = question.Statement;
        op1.Text = question.Op1;
        op2.Text = question.Op2;
        op3.Text = question.Op3;
        op4.Text = question.Op4;
    }
}

class CUI
{
    public void StartTest()
    {
        TestLogic tl = new TestLogic();

        while (true)
        {
            Question question = tl.GetNextQuestion();

            if (question == null)
                break; // test is over

            // Display one question at a time
            Console.Clear();
            Console.WriteLine(question.Statement);
            Console.WriteLine("1: " + question.Op1);
            Console.WriteLine("2: " + question.Op2);
            Console.WriteLine("3: " + question.Op3);
            Console.WriteLine("4: " + question.Op4);

            // take the user's choice
            Console.Write("Select an option: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            tl.CalculateUserMarks(choice);
        }

        // finally display usermarks out of total marks
        Console.Clear();
        Console.WriteLine("You obtained {0} out of {1}", tl.GetUserMarks(), tl.GetTotalMarks());
    }
}

class App10
{
    static void Main()
    {
        //CUI cui = new CUI();
        //cui.StartTest();


        GUI gui = new GUI();
        //gui.Show(); // DOESN't run a While Loop
        gui.ShowDialog(); // Runs a While Loop

    }
}

//-------------------------------------------------------


//class Point2D
//{
//    private int _x, _y; // instance data members

//    public Point2D(/* Point2D this = recv. ref. of the object */)
//    {
//    }

//    public Point2D(/* Point2D this = recv. ref. of the object */ int x, int y)
//    {
//        this.X = x;
//        this.Y = y;
//    }

//    public int X
//    {
//        set // void set_X ( int value ) { this.x = value ; }
//        {
//            if ( value >= 0 )
//                this._x = value;
//        }

//        get // int get_X() { return this.x ; }
//        {
//            return this._x; 
//        }
//    }

//    public int Y
//    {
//        set // void set_Y ( int value ) { this.y = value ; }
//        {
//            if (value >= 0)
//                this._y = value;
//        }

//        get // int get_Y() { return this.y ; }
//        {
//            return this._y;
//        }
//    }

//    public virtual void F1(/* Point2D this = recv. ref. of calling object */)
//    {
//        this.X = 900; 
//    }

//    public void F2(/* Point2D this = recv. ref. of calling object */)
//    {
//        this.X = 900;
//    }

//    public virtual void F3(/* Point2D this = recv. ref. of calling object */)
//    {
//        this.X = 900;
//    }

//}


//// When we inherit Point2D in Point3D:
//// 1. all the data members of Point2D + Point3D will become part of Point3D object
//// 2. all the methods of Point2D + Point3D will work for Point3D object
//class Point3D : Point2D
//{
//    private int _z; 
//    public int Z
//    {
//        set
//        {
//            this._z = value;
//        }
//        get
//        {
//            return this._z;
//        }
//    }

//    public Point3D(/* Point3D this = ref. of calling object */) // : base ( value of this )
//    {

//    }

//    public Point3D ( /* Point3D this */ int x, int y, int z ) : base ( /* value of this */ x, y )
//    {
//        this.Z = z;
//    }

//    public override void F1(/* Point2D this = recv. ref. of calling object */)
//    {
//        this.X = 1000;
//    }

//    public void F2(/* Point2D this = recv. ref. of calling object */)
//    {
//        this.X = 900;
//    }

//    public void F3(/* Point2D this = recv. ref. of calling object */)
//    {
//        this.X = 900;
//    }
//}

//class App10
//{
//    static void Main()
//    {
//        // new creates the object and returns the reference of the object
//        // p1 is a reference variable that holds the reference of the object
//        Point2D p1 = new Point2D();

//        Point2D p2 = new Point2D();

//        Point2D p3 = new Point2D(10, 10);

//        p3.X = 100; // p3.set_X ( 100 ) ;

//        System.Console.WriteLine(p3.X); // p3.get_X() 


//        Point3D q1 = new Point3D();
//        q1.X = 100;
//        q1.Y = 100;
//        q1.Z = 100;

//        Point3D q2 = new Point3D(100, 200, 300);


//        Point2D p4 = new Point3D();
//        p4.F1();
//        p4.F2();
//        p4.F3();
//    }
//}