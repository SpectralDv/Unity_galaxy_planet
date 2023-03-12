
public interface IBase{}
public interface IModel : IBase{}
public interface IState : IBase{string UpdateState(string flag);}


//интерфейсы для камеры
//public interface IGetStateCamera{void GetStateCamera();}
//public interface IUpdateStateCamera{void UpdateStateCamera();}
public interface IStateCamera : IState{}//IGlobalStateCamera,IUpdateStateCamera{}
public interface IGlobalStateCamera : IStateCamera{}
//public interface ICamera : IStateCamera,IUpdateStateCamera{}

//интерфейсы для кнопок
public interface IPressButton{void PressButton();}
public interface IButton : IPressButton{}//,IGlobalStateCamera{}

//интерфейсы для мыши
//public interface IUpdateStateMouse{void UpdateStateMouse();}
public interface IStateMouse : IState{}

//интерфейс для touch андроида
public interface IStateTach : IState{}

