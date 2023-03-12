
using System.Collections.Generic;

/*
//посредник хранит всех сотрудников
//метод который принимает имя исполнителя и комманду для него
//метод которой принимает имя коммандира, комманду, имя для кого комманда
//метод который принимает имя коммандира, комманду для всех сотрудников
public class IMediator 
{
    public virtual void notify(string nameExecutor,string command){}
}

class Mediator : IMediator
{
    public List<BaseModel> listMember;

    public Mediator()
    {
        listMember=new List<BaseModel>();
    }
    public void AddMember(BaseModel member)
    {
        listMember.Add(member);
    }
	public override void notify(string nameExecutor,string command) 
    {
        for (int i = 0; i < listMember.Count; i++)
        {
            if(listMember[i].name == nameExecutor)
            {
                
            }
        }
    }
}

/*
//using namespace SpaceControllerViget;
namespace SpaceMediator
{
	class Person;
	//class ControllerViget;
	//class ModelData;

	class IMediator
    {
    public:
		virtual ~IMediator(){}
		virtual void notify(Person* person,std::string msg){};
    };

	class Person
    {
    protected:
        IMediator* _mediator;
    public:
        Person(IMediator* mediator) : _mediator(mediator){}
        virtual ~Person() {}
        void SetMediator(IMediator* mediator)
        {
            _mediator = mediator;
        }
    };

    class Mediator : public IMediator
    {
    private:
        ControllerViget* _downer;
        ModelData* _uper;
    public:
        Mediator(ControllerViget* downer,ModelData* uper)
        {
            _downer = downer;
            _uper = uper;
            _downer->SetMediator(this);
            _uper->SetMediator(this);
        }
		void notify(Person* person,std::string msg) override
        {
            if(auto uper = dynamic_cast<Uper*>(person))
            {
                if(msg.empty())
                {
                    _downer->setWork(false);
                }
                else
                {
                    _downer->setWork(true);
                }
            }
            if(auto downer = dynamic_cast<Downer*>(person))
            {
                if(msg == "send in uper")
                {
                    _uper->giveCommand("");
                }
            }
        }
    };
}*/