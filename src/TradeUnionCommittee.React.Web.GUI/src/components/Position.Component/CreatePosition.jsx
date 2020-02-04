import React from 'react';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';
import Divider from '@material-ui/core/Divider';

class CreatePosition extends React.Component {

    render() {
        return (
            <React.Fragment>
                <Typography variant="h5" align="left" gutterBottom>
                    Create position
                </Typography>
                <Divider />
                <br />
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        <TextField
                            required
                            variant="outlined"
                            name="name"
                            label="Name position"
                            fullWidth />
                    </Grid>
                    <Grid item xs={12} align="right">
                        <Button variant="contained" color="primary">
                            Create
                        </Button>
                    </Grid>
                </Grid>
            </React.Fragment>
        );
    }
}

export default CreatePosition
