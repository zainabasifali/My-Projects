public abstract class Person {
    private int id;
    private String name;
    static int nextAuthorId = 1;
    static int nextMemberId = 1;
    public Person(int id ,String name) {
        this.id = id;
        this.name = name;
    }

    public static int getNextAuthorId() {
        return nextAuthorId++;
    }

    public static int get_next_MemberId() {
        return nextMemberId++;
    }
    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }
}

