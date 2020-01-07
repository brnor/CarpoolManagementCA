import React from 'react'
import DateTime from 'react-datetime';
import '../../../../node_modules/react-datetime/css/react-datetime.css';
import moment from 'moment';

import classes from './DateTimePicker.module.css';


const dateTime = (props) => {
    
    const valid = (current) => {
        const yesterday = moment().subtract(1, 'day');
        return current.isAfter(yesterday);
    }

    return (
        <DateTime
            className={classes.DateTime}
            timeFormat="HH:mm"
            dateFormat="DD.MM.YYYY"
            isValidDate={valid}
            {...props}
        />
    )
};

export default dateTime;