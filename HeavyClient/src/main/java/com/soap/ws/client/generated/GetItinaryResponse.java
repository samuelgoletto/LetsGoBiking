package com.soap.ws.client.generated;

import javax.xml.bind.JAXBElement;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElementRef;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;

@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
    "getItinaryResult"
})
@XmlRootElement(name = "GetItinaryResponse")
public class GetItinaryResponse {

    @XmlElementRef(name = "GetItinaryResult", namespace = "http://tempuri.org/", type = JAXBElement.class, required = false)
    protected JAXBElement<String> getItinaryResult;

    public JAXBElement<String> getGetItinaryResult() {
        return getItinaryResult;
    }

    public void setGetItinaryResult(JAXBElement<String> value) {
        this.getItinaryResult = value;
    }

}
