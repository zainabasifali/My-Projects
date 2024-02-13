public class Author extends Person{
    private int author_id;
    private String Qualification;
    private int rating;


    public Author(String name, String qualification,int rating) {
        super(getNextAuthorId(),name);
        this.author_id = super.getId();
        this.Qualification = qualification;
        this.rating = rating;
    }

    public int getRating() {
        return rating;
    }

    public void setRating(int rating) {
        this.rating = rating;
    }

    public String getQualification() {
        return Qualification;
    }

    public void setQualification(String qualification) {
        Qualification = qualification;
    }
}
