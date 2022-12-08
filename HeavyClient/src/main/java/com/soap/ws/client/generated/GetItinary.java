package com.soap.ws.client.generated;

import javax.xml.bind.JAXBElement;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElementRef;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;

@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
    "origin",
    "destination"
})
@XmlRootElement(name = "GetItinary")
public class GetItinary {
    @XmlElementRef(name = "origin", namespace = "http://tempuri.org/", type = JAXBElement.class, required = false)
    protected JAXBElement<String> origin;
    @XmlElementRef(name = "destination", namespace = "http://tempuri.org/", type = JAXBElement.class, required = false)
    protected JAXBElement<String> destination;

    public JAXBElement<String> getOrigin() {
        return origin;
    }

    public void setOrigin(JAXBElement<String> value) {
        this.origin = value;
    }

    public JAXBElement<String> getDestination() {
        return destination;
    }

    public void setDestination(JAXBElement<String> value) {
        this.destination = value;
    }
}
