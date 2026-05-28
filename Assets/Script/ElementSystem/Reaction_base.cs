using UnityEngine;

public class Reaction_base
{
    Element_reaction currentReact = Element_reaction.None_react;
     protected float reactionDuration;
     protected float reactionTimer;
    private Enemy enemy=null;
    private Player player=null;
    protected bool isReact;
    public virtual void processReact(Entity_Current_buff entity)
    {
        isReact = true;
        GetTureNameOfEntity(entity);
        reactionTimer = reactionDuration;
    }

    private void GetTureNameOfEntity(Entity_Current_buff entity)
    {
        if (entity.GetComponent<Player>() != null)
        {
            player = (Player)entity.GetComponent<Player>();
        }
        else if (entity.GetComponent<Enemy>() != null)
        {
            enemy = (Enemy)entity.GetComponent<Enemy>();
        }
    }

    public virtual void Update()
    {
        reactionTimer-= Time.deltaTime;
        if(reactionTimer<0)
        {
            isReact=false;
        }
    }
}
