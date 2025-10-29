using System;

[System.Serializable]
public class WordsClass
{
    public String correct;
    public String basic;
    public String option1;
    public String option2;
    public String option3;

    public WordsClass(String correct, String basic, String option1, String option2, String option3)
    {
        this.correct = correct;
        this.basic = basic;
        this.option1 = option1;
        this.option2 = option2;
        this.option3 = option3;
    }

    public bool isCorrectAccent(int option)
    {
        if (option == 1 && option1.Equals(correct)) return true;
        if (option == 2 && option2.Equals(correct)) return true;
        if (option == 3 && option3.Equals(correct)) return true;
        return false;
    }
}
