package com.soap.ws.client.generated;

import javax.jws.WebMethod;
import javax.jws.WebParam;
import javax.jws.WebResult;
import javax.jws.WebService;
import javax.xml.bind.annotation.XmlSeeAlso;
import javax.xml.ws.RequestWrapper;
import javax.xml.ws.ResponseWrapper;

@WebService(name = "IGoBikeService", targetNamespace = "http://tempuri.org/")
@XmlSeeAlso({
    ObjectFactory.class
})
public interface IGoBikeService {
    @WebMethod(operationName = "GetItinary", action = "http://tempuri.org/IGoBikeService/GetItinary")
    @WebResult(name = "GetItinaryResult", targetNamespace = "http://tempuri.org/")
    @RequestWrapper(localName = "GetItinary", targetNamespace = "http://tempuri.org/", className = "com.soap.ws.client.generated.GetItinary")
    @ResponseWrapper(localName = "GetItinaryResponse", targetNamespace = "http://tempuri.org/", className = "com.soap.ws.client.generated.GetItinaryResponse")
    public String getItinary(
        @WebParam(name = "origin", targetNamespace = "http://tempuri.org/")
        String origin,
        @WebParam(name = "destination", targetNamespace = "http://tempuri.org/")
        String destination);
}