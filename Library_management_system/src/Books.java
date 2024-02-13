public abstract class Books {
    private int Id ;
    static int counter = 1;
    private String Title;
    private Author author;
    int no_of_copies;
    private String Category;

    Books(String Category ,String Title , Author a , int no_of_copies){
        this.no_of_copies = no_of_copies;
        this.author = a;
        this.Title =Title;
        this.Id  = counter ++;
        this.Category = Category;
    }

    public void Set_Title(String title){
        Title = title;
    }
    public void setNo_of_copies(int no_of_copies) {
        this.no_of_copies = no_of_copies;
    }

    public void setId(int id) {
        Id = id;
    }

    public void setAuthor(Author author) {
        this.author = author;
    }
    public void setCategory(String category) {
        Category = category;
    }
    public int Get_Id(){
        return Id;
    }
    public String Get_Title(){
        return Title;
    }

    public int getNo_of_copies() {
        return no_of_copies;
    }

    public Author getAuthor() {
        return author;
    }

    public String getCategory() {
        return Category;
    }

}
