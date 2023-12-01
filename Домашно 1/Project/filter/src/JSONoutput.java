import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonElement;
import com.google.gson.JsonParser;

import java.io.FileNotFoundException;
import java.io.PrintWriter;

public class JSONoutput {
    public void write(String input, String output) throws FileNotFoundException {
        PrintWriter pw = new PrintWriter(output);
        JsonParser parser = new JsonParser();
        Gson gson = new GsonBuilder().setPrettyPrinting().create();
        JsonElement el = parser.parse(input.toString());
        String resultString = gson.toJson(el);
        pw.write(resultString);
        pw.flush();
        pw.close();
    }
}
