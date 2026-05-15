
public class Port
{
    public PortType Type;
    public ANode Content;
    public Connection Connection;

    public Port(PortType type, ANode content, Connection connection)
    {
        Type = type;
        Content = content;
        Connection = connection;
    }
    
    
}

public enum PortType { IN , OUT}
