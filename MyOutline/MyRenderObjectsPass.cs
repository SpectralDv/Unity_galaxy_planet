using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class MyRenderObjectsPass : ScriptableRenderPass//: MonoBehaviour
{
    //для новой цели рендера
    private RenderTargetHandle _destination;

    public MyRenderObjectsPass(RenderTargetHandle destination)
    {
        _destination = destination;
    }

    //логика работы внедрения рисунка в кадр 
    public override void Configure(CommandBuffer cmd,RenderTextureDescriptor cameraTextureDescriptor)
    {
        //замена цели рндера
        cmd.GetTemporaryRT(_destination.id,cameraTextureDescriptor);
        //указание цели
        ConfigureTarget(_destination.Identifier());
        ConfigureClear(ClearFlag.All,Color.clear);
    }

    //указывает цель рендеринга и создает временные текстуры
    public override void Execute(ScriptableRenderContext context, ref RenderingData RenderingData)
    {

    }
}
