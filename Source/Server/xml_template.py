from string import Template

template_options = Template(
    '<options>'
    '<id>${id}</id>'
    '<messageType>${messageType}</messageType>'
    '<command>${command}</command>'
    '<channel>${channel}</channel>'
    '<user>${user}</user>'
    '</options>'
    )

template_message = Template('<message>${message}</message>')


# mapping example:
# d = dict(
#     id='5',
#     messageType='2',
#     command='2',
#     channel='Channelname',
#     user='Bob'
#     )


def xml_options(mapping):
    global template_options
    return template_options.substitute(mapping)

def xml_message(msg):
    global template_message
    mapping = dict(
        message=msg
        )
    return template_message.substitute(mapping)