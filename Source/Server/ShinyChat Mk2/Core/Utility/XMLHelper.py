from string import Template
import xml.etree.ElementTree as ET

template_options = Template('<options>'
                            '<id>${identifier}</id>'
                            '<messageType>${messageType}</messageType>'
                            '<command>${command}</command>'
                            '<channel>${channel}</channel>'
                            '<user>${user}</user>'
                            '</options>')

template_message = Template('<message>${message}</message>')

# mapping takes a dictionary
def xml_options(mapping):
    return template_options.substitute(mapping)

def xml_message(mapping):
    return template_message.substitute(mapping)

def parse(xmlString):
    root = ET.fromstring(xmlString)
    return {child.tag: child.text for child in root}

def get_roottext(xmlString):
    root = ET.fromstring(xmlString)
    return root.text