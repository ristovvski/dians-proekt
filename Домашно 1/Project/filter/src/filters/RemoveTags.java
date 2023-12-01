package filters;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

import java.util.*;

public class RemoveTags implements Filter<JSONObject>{

    @Override
    public JSONObject execute(JSONObject input) {
        JSONArray inputArray = (JSONArray) input.get("elements");
        Iterator itr = inputArray.iterator();
        while (itr.hasNext())
        {
            Map element = (Map)itr.next();
            Map tags = (Map)element.get("tags");
            element.put("name",tags.get("name"));
            element.put("type",tags.get("historic"));
            element.remove("tags");
        }
        return input;
    }
}
