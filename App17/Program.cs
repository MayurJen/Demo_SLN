using System;
using System.Drawing;
using System.Windows.Forms;


class App17
{
    static void Main()
    {
        GUI gui = new GUI();
        gui.ShowDialog();
    }
}

class GUI : Form
{
    TextBox input = new TextBox();
    Button push = new Button();
    Button pop = new Button();
    Label res = new Label();
    abb.Stack<int> s1 = new abb.Stack<int>(); // s1 holds reference of the object created using new

    public GUI()
    {
        this.Controls.Add(input);
        this.Controls.Add(push);
        this.Controls.Add(pop);
        this.Controls.Add(res);

        input.Location = new Point(10, 10);
        push.Location = new Point(10, 50);
        pop.Location = new Point(100, 50);
        res.Location = new Point(10, 90);

        push.Text = "Push";
        pop.Text = "Pop";
        push.Click += PushClicked;
        pop.Click += PopClicked;

    }

    public void PushClicked(object sender, EventArgs e)
    {
        try
        {
            s1.Push(Convert.ToInt32(input.Text));
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

    }
    public void PopClicked(object sender, EventArgs e)
    {
        try
        {
            res.Text = Convert.ToString(s1.Pop());
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

}