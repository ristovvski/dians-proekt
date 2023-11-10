import filters.*;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import java.io.FileReader;
import java.io.IOException;

public class Main {
    public static void main(String[] args) throws IOException, ParseException {
        JSONoutput jsonOut=new JSONoutput();

        RemoveAttributes removeAttributes = new RemoveAttributes();
        RemoveTags removeTags = new RemoveTags();
        RemoveIncomplete removeIncomplete = new RemoveIncomplete();
        FilterByType filterByType = new FilterByType();

        Pipe pipe1 = new Pipe<JSONObject>();
        pipe1.addFilter(removeAttributes);
        pipe1.addFilter(removeTags);
        pipe1.addFilter(removeIncomplete);

        Pipe pipe2 = new Pipe<JSONObject>();
        pipe2.addFilter(removeAttributes);
        pipe2.addFilter(filterByType);
        pipe2.addFilter(removeIncomplete);

        Object obj1 = new JSONParser().parse(new FileReader("src/data/historic.json"));
        Object obj2 = new JSONParser().parse(new FileReader("src/data/tourism.json"));

        JSONObject jo1 = (JSONObject) obj1;
        JSONObject jo2 = (JSONObject) obj2;

        JSONObject result1 = (JSONObject) pipe1.runFilters(jo1);
        jsonOut.write(result1.toString(),"src/result1.json");

        JSONObject result2 = (JSONObject) pipe2.runFilters(jo2);
        jsonOut.write(result2.toString(),"src/result2.json");

        JSONArray jsonArray1 = (JSONArray) result1.get("elements");
        JSONArray jsonArray2 = (JSONArray) result2.get("elements");
        jsonArray1.addAll(jsonArray2);

        jsonOut.write(jsonArray1.toString(),"src/result.json");
    }
}