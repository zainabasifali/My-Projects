import java.time.LocalDate;
import java.util.Date;
import java.time.LocalDate;
public class Reservation {
    private Books b;
    private Member m;
    private LocalDate issue_date;
    private LocalDate return_date;
    static int fine = 1000;

    public Reservation(Books b, Member m, LocalDate issue_date, LocalDate return_date) {
        this.b = b;
        this.m = m;
        this.issue_date = issue_date;
        this.return_date = return_date;
    }

    public LocalDate getIssue_date() {
        return issue_date;
    }

    public void setIssue_date(LocalDate issue_date) {
        this.issue_date = issue_date;
    }

    public LocalDate getReturn_date() {
        return return_date;
    }

    public void setReturn_date(LocalDate return_date) {
        this.return_date = return_date;
    }

    public Books getB() {
        return b;
    }

    public void setB(Books b) {
        this.b = b;
    }

    public Member getM() {
        return m;
    }

    public void setM(Member m) {
        this.m = m;
    }
}
