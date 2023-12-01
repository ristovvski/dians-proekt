package filters;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

import java.util.Iterator;
import java.util.Map;

public class RemoveIncomplete implements Filter<JSONObject>{
    @Override
    public JSONObject execute(JSONObject input) {
        JSONArray inputArray = (JSONArray) input.get("elements");
        Iterator itr = inputArray.iterator();
        while (itr.hasNext())
        {
            Map element = (Map)itr.next();
            if(element.get("name") == null){
                itr.remove();
            }
        }
        return input;
    }
}
