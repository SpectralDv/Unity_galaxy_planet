using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ViewCamera,ViewGalaxy,ViewSolarSystem,ViewPlanet отслеживают глобальные состояния в Update
//1.selectsolarsystem
//скрывать солнечную систему, только по нажатию при выборе другой солнечной системы
//2.selectplanet or selectsolarsystem
//скрывать планету, только по нажатию при выборе другой планеты или другой солнечной системы

//если галактику, солнечные системы и планеты создать в 0,0,0 координатах
//камера должна сбрасывать позицию в 0,0+h,0
///////необходимо передать камере позицию галактики, солнечной системы или планеты

//0.creategalaxy
//если галатика, солнечная система и планеты, иницилизирована в памяти, созданы и скрыты

//init задает ViewGalaxy в Start
//create задает ViewGalaxy, после создания всех SolarSystem на сцене 
enum eGLOBALSTATE
{
    init,
    create,
    info,
    targetGO,
    planet,
    solarsystem,
    galaxy,
    buttonselectsolarsystem,
    buttonselectplanet,
    showgalaxy, //показывает галактику
    galaxytosolarsystem, 
    solarsystemtogalaxy,
    showsolarsystem, //показывает солнечную систему
    solarsystemtoplanet, 
    planettosolarsystem,
    showplanet, //показывает планету
    selectsolarsystem,
    selectplanet,
}

//состояния игрового пространства
enum eSTATEGAME
{
    mainmenu,
    galaxy,
    solarsystem,
    planet,
}



//галактика инициализируется в координатах галактики на сцене
//солнечные системы инициализируются в координатах галактики на сцене
//модель галактика хранит звезды и солнечные системы

enum eGALAXYSTATE
{
    init,//выделение памяти
    create,//объекты на сцене созданы и скрыты
    show,
    hide,
    galaxy,
    solarsystem,
    planet,
}

//создание солнечной системы на сцене 
//производится от ViewGalaxy через ModelGalaxy

enum eSOLARSYSTEM
{
    init, //если создан в памяти
    create, //если создан на сцене
    hide, //если скрыт на сцене
    show, //если виден на сцене
    select, //если выбран
}

//при смене события изменять позицию камеры
enum eCAMERASTATE
{
    empty,
    init, //задает начальные координаты камеры
    planet,
    solarsystem,
    galaxy,
    targetGO,
}

enum ePANELSTATE
{
    empty,
    solarsystem,
    planet,
}

enum eBUTTONSTATE
{
    empty,
    selectsolarsystem,
    selectplanet,
}

public enum eSTATEMOUSE
{
    idle, //в покое
    press, //нажать
    hold, //удерживать
    release, //отпустить
    scroll, //зум
}