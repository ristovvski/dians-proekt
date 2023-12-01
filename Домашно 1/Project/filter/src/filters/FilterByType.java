package filters;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

import java.util.Iterator;
import java.util.Map;

public class FilterByType implements Filter<JSONObject>{
    @Override
    public JSONObject execute(JSONObject input) {
        JSONArray inputArray = (JSONArray) input.get("elements");
        Iterator itr = inputArray.iterator();
        while (itr.hasNext())
        {
            Map element = (Map)itr.next();
            Map tags = (Map)element.get("tags");
            if((tags.containsKey("amenity"))){
                if(tags.get("amenity").equals("place_of_worship")){
                    element.put("type",tags.get("amenity"));
                    element.put("name",tags.get("name"));
                    element.remove("tags");
                    continue;
                }
            }
            if((tags.containsKey("memorial"))){
                if(tags.get("memorial").equals("statue")){
                    element.put("type",tags.get("memorial"));
                    element.put("name",tags.get("name"));
                    element.remove("tags");
                    continue;
                }
            }
            if((tags.containsKey("tourism"))){
                if(tags.get("tourism").equals("museum")){
                    element.put("type",tags.get("tourism"));
                    element.put("name",tags.get("name"));
                    element.remove("tags");
                    continue;
                }
            }
            itr.remove();
        }
        return input;
    }
}
