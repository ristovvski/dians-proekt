package filters;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.Map;

public class RemoveAttributes implements Filter<JSONObject>{
    @Override
    public JSONObject execute(JSONObject input) {
        JSONObject result = new JSONObject();
        JSONArray jsonArray = new JSONArray();
        JSONArray inputArray = (JSONArray) input.get("elements");
        Iterator itr = inputArray.iterator();
        Iterator<Map.Entry> itr1 = null;
        while (itr.hasNext())
        {
            Map m = new LinkedHashMap(4);
            itr1 = ((Map) itr.next()).entrySet().iterator();
            while (itr1.hasNext()) {
                Map.Entry pair = itr1.next();
                if(pair.getKey().equals("lon") || pair.getKey().equals("lat") || pair.getKey().equals("tags")) {
                    m.put(pair.getKey(), pair.getValue());
                }
            }
            jsonArray.add(m);
        }
        result.put("elements",jsonArray);
        return result;
    }
}
