import React from 'react';
import { Table, Button } from 'react-bootstrap';

const dateFormatter = new Intl.DateTimeFormat('hr-HR', {
    year: 'numeric',
    month: "2-digit",
    day: "2-digit",
    hour: "2-digit",
    minute: "2-digit",
});

const rideshares = (props) => (
    <div>
        <Table className="table table-striped">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Start Location</th>
                    <th>End Location</th>
                    <th>Start Date</th>
                    <th>End Date</th>
                    <th></th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {props.rideshares.map(rideshare => (
                    <tr key={rideshare.id}>
                        <td>{rideshare.id}</td>
                        <td>{rideshare.startLocation}</td>
                        <td>{rideshare.endLocation}</td>
                        <td>{dateFormatter.format(new Date(rideshare.startDate))}</td>
                        <td>{dateFormatter.format(new Date(rideshare.endDate))}</td>
                        <td><Button onClick={() => props.modalShowHandler("edit", rideshare.id)}>Edit</Button></td>
                        <td><Button color="danger" onClick={() => props.modalShowDeleteHandler(rideshare.id)}>Delete</Button></td>
                    </tr>
                ))}
            </tbody>
        </Table>
    </div>
);

export default rideshares;