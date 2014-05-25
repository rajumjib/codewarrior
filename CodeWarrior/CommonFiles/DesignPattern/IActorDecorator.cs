public interface IActorDecorator
{
	void Act();
}

public class FirstActorDecorator : IActorDecorator
{
	private readonly IActorDecorator _actorDecorator;

	public FirstActorDecorator(IActorDecorator actorDecorator)
	{
		_actorDecorator = actorDecorator;
	}

	public void Act()
	{
		//do somthing
		_actorDecorator.Act();
	}
}

public class SecondActorDecorator : IActorDecorator
{
	private readonly IActorDecorator _actorDecorator;

	public SecondActorDecorator(IActorDecorator actorDecorator)
	{
		_actorDecorator = actorDecorator;
	}

	public void Act()
	{
		//do somthing else
		_actorDecorator.Act();
	}
}

public class ThirdActorDecorator : IActorDecorator
{
	public ThirdActorDecorator()
	{
		
	}

	public void Act()
	{
		//do one last thing
	}
}