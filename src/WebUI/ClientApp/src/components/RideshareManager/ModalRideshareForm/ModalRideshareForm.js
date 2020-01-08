import React, { Component } from 'react'
import { Modal, Form, FormGroup, Spinner, Button } from 'react-bootstrap'
import DateTimePicker from '../../UI/DateTimePicker/DateTimePicker';
import Select from 'react-select';

import classes from './ModalRideshareForm.module.css';

class ModalRideshareForm extends Component {

    render () {
        let id, startLocation, endLocation, startDate, endDate = "";
        let car = {}; let employees = [];
        if (this.props.rideshareDetail) {
            ({id, startLocation, endLocation, startDate, endDate, car, employees} = this.props.rideshareDetail);
            startDate = new Date(startDate);
            endDate = new Date(endDate);
        }

        const title = this.props.action === "add" 
            ? "Add new rideshare"
            : "Edit rideshare (ID: " + id + ")";
        let form = this.props.error
            ? <p>Rideshare cannot be loaded!</p>
            : <Spinner animation="border" />

        if (!this.props.loading) {
            form = (
                <Form onSubmit={this.props.action === "edit" ? this.props.onSubmitEdit : this.props.onSubmitAdd}>
                    <Modal.Body className={classes}>
                        <FormGroup>
                            {/* Start location */}
                            <Form.Label>Start Location</Form.Label>
                            <Form.Control
                                type="text"
                                name="startLocation"
                                value={startLocation}
                                onChange={this.props.onChangeField} />
                        </FormGroup>
                        <FormGroup>
                            {/* End location */}
                            <Form.Label>End Location</Form.Label>
                            <Form.Control
                                type="text"
                                name="endLocation"
                                value={endLocation}
                                onChange={this.props.onChangeField} />
                        </FormGroup>
                        <FormGroup>
                            {/* Start date */}
                            <label>Start date: </label>
                            <DateTimePicker
                                name="startDate"
                                value={startDate}
                                onChange={this.props.onChangeStartDate} />
                        </FormGroup>
                        <FormGroup>
                            {/* End date */}
                            <label>End date: </label>
                            <DateTimePicker
                                name="endDate"
                                value={endDate}
                                onChange={this.props.onChangeEndDate} />
                        </FormGroup>
                        <FormGroup>
                            {/* Car */}
                            <label>Car: </label>
                            <Select
                                onChange={this.props.onChangeCar}
                                options={this.props.allCars.map((c) => {
                                    return (
                                        { value: c.id, label: c.name + " (" + c.numberOfSeats + ")" }
                                    )
                                })}
                                value={{ value: car.id, label: car.name + " (" + car.numberOfSeats + ")" }} />
                        </FormGroup>
                        <FormGroup>
                            {/* Employees */}
                            <label>Employees: </label>
                            <Select
                                isMulti
                                onChange={this.props.onChangeEmployees}
                                options={this.props.allEmployees.map((e) => {
                                    let driverTag = e.isDriver ? " [D]" : "";
                                    return (
                                        { value: e.id, label: e.fullName + driverTag }
                                    );
                                })}
                                value={employees.map((e) => {
                                    let driverTag = e.isDriver ? " [D]" : "";
                                    return (
                                        { value: e.id, label: e.fullName + driverTag }
                                    );
                                })} />
                        </FormGroup>
                    </Modal.Body>
                    <Modal.Footer>
                        { /* Submit and Cancel buttons */}
                        <Button type="submit" variant="primary">Submit</Button>
                        <Button variant="danger" onClick={this.props.handleClose}>Cancel</Button>
                    </Modal.Footer>
                </Form>
            );
        }

        return (
            <Modal show={this.props.show} onHide={this.props.handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>{title}</Modal.Title>
                </Modal.Header>
                {form}
            </Modal>
        );
    };
}

export default ModalRideshareForm;