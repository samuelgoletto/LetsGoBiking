package com.soap.ws.client.generated;

import java.net.MalformedURLException;
import java.net.URL;
import javax.xml.namespace.QName;
import javax.xml.ws.Service;
import javax.xml.ws.WebEndpoint;
import javax.xml.ws.WebServiceClient;
import javax.xml.ws.WebServiceException;
import javax.xml.ws.WebServiceFeature;

@WebServiceClient(name = "GoBikeService", targetNamespace = "http://tempuri.org/", wsdlLocation = "http://localhost:8733/Design_Time_Addresses/RoutingServer/GoBikeService/?wsdl")
public class GoBikeService
    extends Service
{
    private final static URL GOBIKESERVICE_WSDL_LOCATION;
    private final static WebServiceException GOBIKESERVICE_EXCEPTION;
    private final static QName GOBIKESERVICE_QNAME = new QName("http://tempuri.org/", "GoBikeService");

    static {
        URL url = null;
        WebServiceException e = null;
        try {
            url = new URL("http://localhost:8733/Design_Time_Addresses/RoutingServer/GoBikeService/?wsdl");
        } catch (MalformedURLException ex) {
            e = new WebServiceException(ex);
        }
        GOBIKESERVICE_WSDL_LOCATION = url;
        GOBIKESERVICE_EXCEPTION = e;
    }

    public GoBikeService() {
        super(__getWsdlLocation(), GOBIKESERVICE_QNAME);
    }

    public GoBikeService(WebServiceFeature... features) {
        super(__getWsdlLocation(), GOBIKESERVICE_QNAME, features);
    }

    public GoBikeService(URL wsdlLocation) {
        super(wsdlLocation, GOBIKESERVICE_QNAME);
    }

    public GoBikeService(URL wsdlLocation, WebServiceFeature... features) {
        super(wsdlLocation, GOBIKESERVICE_QNAME, features);
    }

    public GoBikeService(URL wsdlLocation, QName serviceName) {
        super(wsdlLocation, serviceName);
    }

    public GoBikeService(URL wsdlLocation, QName serviceName, WebServiceFeature... features) {
        super(wsdlLocation, serviceName, features);
    }

    @WebEndpoint(name = "BasicHttpBinding_IGoBikeService")
    public IGoBikeService getBasicHttpBindingIGoBikeService() {
        return super.getPort(new QName("http://tempuri.org/", "BasicHttpBinding_IGoBikeService"), IGoBikeService.class);
    }

    @WebEndpoint(name = "BasicHttpBinding_IGoBikeService")
    public IGoBikeService getBasicHttpBindingIGoBikeService(WebServiceFeature... features) {
        return super.getPort(new QName("http://tempuri.org/", "BasicHttpBinding_IGoBikeService"), IGoBikeService.class, features);
    }

    private static URL __getWsdlLocation() {
        if (GOBIKESERVICE_EXCEPTION!= null) {
            throw GOBIKESERVICE_EXCEPTION;
        }
        return GOBIKESERVICE_WSDL_LOCATION;
    }

}
