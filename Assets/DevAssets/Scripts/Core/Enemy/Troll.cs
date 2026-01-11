using System.Threading.Tasks;

public class Troll : Enemy
{
    public override void Attack()
    {
        
    }

    private async void AttackSequance()
    {
        if(!_canAttack) return;
        
        _canAttack = false;
        await Task.Delay(_attackSequance * 1000);
        _canAttack = true;
    }
}