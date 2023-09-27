namespace QuizAPI.Data;

public record User(
    int Identifier, 
    string Username, 
    string Password
);