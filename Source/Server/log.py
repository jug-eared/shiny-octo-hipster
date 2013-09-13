# Hardcoded log settings
import logging

def init_log(name):
    """Returns Logger object

    supported severities:
    debug, info, warn, error, critical"""

    logger = logging.getLogger(name)
    logger.setLevel(logging.DEBUG)

    # Log to file 'server.log' (threshold=DEBUG, mode=append)
    fh = logging.FileHandler('server.log')
    fh.setLevel(logging.DEBUG)

    # Log to console (threshold=ERROR)
    ch = logging.StreamHandler()
    ch.setLevel(logging.ERROR)

    # Format for log entries
    formatter = logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s')
    ch.setFormatter(formatter)
    fh.setFormatter(formatter)

    logger.addHandler(ch)
    logger.addHandler(fh)

    return logger
