﻿using System;
using System.Threading;

namespace RPGgame
{
    //1) «Добавить здоровье». Суть этого заклинания – увеличить текущее значение
    //здоровья какого-либо персонажа на заданную величину или до предела,
    //задаваемого текущим значением маны.На единицу добавленного здоровья
    //расходуется две единицы маны.

    class Addhelth : Spell
    {
        override public void DoMAgicThing(int Damage, MagicCharacter person)//Damage - потраченные мп
        {

            
        }

        //public void HealthUp(object ob)
        //{
        //    if (ob is CharacterInfo)
        //    {
        //        int available = curMP / 2;
        //        //(int)Math.Ceiling((double)curMP / 2);

        //        if (ActivateSpell(available))
        //        {
        //            if ((ob as CharacterInfo).CurrentHealth + available == CharacterInfo.MaxHealth)
        //            {
        //                CurrentMagicPower = 0;
        //                CurrentHealth = CharacterInfo.MaxHealth;
        //            }
        //            if ((ob as CharacterInfo).CurrentHealth + available > CharacterInfo.MaxHealth)
        //            {
        //                CurrentHealth = CharacterInfo.MaxHealth;
        //                CurrentMagicPower -= 2 * (CharacterInfo.MaxHealth - (ob as CharacterInfo).CurrentHealth);
        //            }
        //            if ((ob as CharacterInfo).CurrentHealth + available < CharacterInfo.MaxHealth)
        //            {
        //                CurrentMagicPower = 0;
        //                CurrentHealth += available;
        //            }
        //        }
        //    }
        //}
    }
    //2) «Вылечить». Суть этого заклинания – перевести какого-либо персонажа из
    //состояния «болен» в состояние «здоров или ослаблен». Текущая величина
    //здоровья не изменяется.Заклинание требует 20 единиц маны.

    class ToCure : Spell
    {
        override public void DoMAgicThing(MagicCharacter person)
        {
            MinMan = 20;

            if (person.state == CharacterInfo.State.sick)
            {
                if (person.CurrentHealth < 10)
                {
                    person.state = CharacterInfo.State.weakend;
                }
                if (person.CurrentHealth >= 10)
                {
                    person.state = CharacterInfo.State.normal;
                }
                person.CurrentMagicPower -= MinMan;//не знаю где проходит проверка на достаточность маны
            }

        }
    }
    //3) «Противоядие». Суть этого заклинания – перевести какого-либо персонажа
    //из состояния «отравлен» в состояние «здоров или ослаблен». Текущая
    //величина здоровья не изменяется.Заклинание требует 30 единиц маны.

    class Antidot : Spell
    {
        public Antidot(ref MagicCharacter person)
        {
            person.state = CharacterInfo.State.normal;
            person.CurrentMagicPower -= 30;//не знаю где проходит проверка на достаточность маны
        }
    }
    //4) «Оживить». Суть этого заклинания – перевести какого-либо персонажа из
    //состояния «мертв» в состояние «здоров или ослаблен». Текущая величина
    //здоровья становится равной 1. Заклинание требует 150 единиц маны.

    class Revive : Spell
    {
        public Revive(ref MagicCharacter person)
        {
            person.state = CharacterInfo.State.normal;
            person.CurrentMagicPower -= 150;//не знаю где проходит проверка на достаточность маны
            person.CurrentHealth = 1;
        }
    }
    //5) «Броня». Персонаж, на которого обращено заклинание, становится
    //неуязвимым в течение некоторого промежутка времени, определяемого
    //силой заклинания.Заклинание требует 50 единиц маны на единицу времени.

    class Armor : Spell
    {
        int Time;
        public override void DoMAgicThing(int time, MagicCharacter person)
        {
            int firstprotection = person.Protection;
            person.Protection = 100;
            Time = time;
            Thread t = new Thread(SleepNow);
            t.Start();
            t.Join();
            person.Protection = firstprotection;
        }
        //public override void Armor(int time, MagicCharacter person)//луше сделать конструктор а не метод наверное
        //{
        //    int firstprotection = person.Protection;
        //    person.Protection = 100;
        //    Time = time;
        //    Thread t = new Thread(SleepNow);
        //    t.Start();
        //    t.Join();
        //    person.Protection = firstprotection;
        //}
        private void SleepNow()
        {
            Thread.Sleep(Time);
        }
    }
    //6) «Отомри!» Суть этого заклинания – перевести какого-либо персонажа из
    //состояния «парализован» в состояние «здоров или ослаблен». Текущая
    //величина здоровья становится равной 1. Заклинание требует 85 единиц маны.
    class NotExpeliarmus : Spell
    {
        public NotExpeliarmus(ref MagicCharacter person)
        {
            if (person.state == CharacterInfo.State.paralyzed)
            {
                person.state = CharacterInfo.State.normal;
                person.CurrentMagicPower -= 85;//не знаю где проходит проверка на достаточность маны
                person.CurrentHealth = 1;
            }
        }
    }
}
