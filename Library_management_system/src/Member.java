public class Member extends Person{
    private int Member_id;
    private String Member_Address;
    private String Member_Phone_number;
    private boolean member_ship_status;

    public Member(String member_Name, String member_Address, String member_Phone_number, boolean member_ship_status) {
        super(get_next_MemberId(),member_Name);
        this.Member_id = super.getId();
        Member_Address = member_Address;
        Member_Phone_number = member_Phone_number;
        this.member_ship_status = member_ship_status;
    }

    public String getMember_Address() {
        return Member_Address;
    }

    public void setMember_Address(String member_address) {
        Member_Address = member_address;
    }

    public String getMember_Phone_number() {
        return Member_Phone_number;
    }

    public void setMember_Phone_number(String member_Phone_number) {
        Member_Phone_number = member_Phone_number;
    }

    public boolean isMember_ship_status() {
        return member_ship_status;
    }

    public void setMember_ship_status(boolean member_ship_status) {
        this.member_ship_status = member_ship_status;
    }
}

