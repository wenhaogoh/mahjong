using System.Collections.Generic;

public class RequestQueue
{
    private const int MAX_QUEUE_SIZE = 3;
    private List<Request> requestQueue;
    private Player offeringPlayer;

    public RequestQueue()
    {
        this.requestQueue = new List<Request>();
    }
    public void SetOfferingPlayer(Player offeringPlayer)
    {
        this.offeringPlayer = offeringPlayer;
    }
    public void Add(Request request)
    {
        requestQueue.Add(request);
    }
    public bool IsFull()
    {
        return requestQueue.Count >= MAX_QUEUE_SIZE;
    }
    public Request GetHighestPriorityRequest()
    {
        requestQueue.Sort();
        return requestQueue[requestQueue.Count - 1];
    }
    public void Reset()
    {
        requestQueue = new List<Request>();
        offeringPlayer = null;
    }
}