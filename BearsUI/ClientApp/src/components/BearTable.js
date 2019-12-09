import React, { Component } from 'react'
import axios from 'axios';

class BearTable extends Component {
    EditBear(bear, action) {
   
        action(bear);
            
          

    }
    DeleteBear(bear, action) {
        axios.delete('/bears/' + bear.id)
            .then(function (response) {

                action();
            })
            .catch(function (error) {
                console.log(error);
            });

    }
    render() {
        var t = this.props.bears;
        console.log(t)
        return(
            <table>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Type</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        this.props.bears.length > 0 ? (
                            this.props.bears.map(bear => (
                                <tr key={bear.id}>
                                    <td>{bear.name}</td>
                                    <td>{bear.typeName}</td>
                                    <td>
                                        <button onClick={() =>
                                            this.EditBear(bear, this.props.doEdit)}
                                            className="button muted-button">Edit</button>&nbsp;
                                        <button onClick={()=>
                                            this.DeleteBear(bear, this.props.action)}
                                            className="button muted-button">Delete</button>
                                    </td>
                                </tr>
                            ))
                        ) : (
                                <tr>
                                    <td colSpan={3}>No Bears</td>
                                </tr>
                            )}
                      
                </tbody>
            </table>
        )
    }
}
export default BearTable