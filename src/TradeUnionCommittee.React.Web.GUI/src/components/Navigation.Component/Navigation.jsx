import React from 'react';
import { Link } from "react-router-dom";
import clsx from 'clsx';
import { withStyles } from '@material-ui/core/styles';
import { withRouter } from "react-router-dom";
import Drawer from '@material-ui/core/Drawer';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import List from '@material-ui/core/List';

import Typography from '@material-ui/core/Typography';
import Divider from '@material-ui/core/Divider';
import IconButton from '@material-ui/core/IconButton';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import Collapse from '@material-ui/core/Collapse';

import MenuIcon from '@material-ui/icons/Menu';
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft';
import ExpandLess from '@material-ui/icons/ExpandLess';
import ExpandMore from '@material-ui/icons/ExpandMore';
import PersonAddIcon from '@material-ui/icons/PersonAdd';
import CategoryIcon from '@material-ui/icons/Category';
import BarChartIcon from '@material-ui/icons/BarChart';
import AssessmentIcon from '@material-ui/icons/Assessment';
import AccessibleIcon from '@material-ui/icons/Accessible';
import AttachMoneyIcon from '@material-ui/icons/AttachMoney';
import AccessibilityIcon from '@material-ui/icons/Accessibility';
import GolfCourseIcon from '@material-ui/icons/GolfCourse';
import TodayIcon from '@material-ui/icons/Today';
import EventIcon from '@material-ui/icons/Event';
import EventAvailableIcon from '@material-ui/icons/EventAvailable';
import EventNoteIcon from '@material-ui/icons/EventNote';
import DateRangeIcon from '@material-ui/icons/DateRange';
import SubdirectoryArrowRightIcon from '@material-ui/icons/SubdirectoryArrowRight';
import HomeWorkIcon from '@material-ui/icons/HomeWork';
import HotelIcon from '@material-ui/icons/Hotel';
import PeopleIcon from '@material-ui/icons/People';
import DashboardIcon from '@material-ui/icons/Dashboard';
import StorageIcon from '@material-ui/icons/Storage';
import SearchIcon from '@material-ui/icons/Search';
import ExitToAppIcon from '@material-ui/icons/ExitToApp';

import {
  AUTH,
  APP_CREATE_EMPLOYEE,
  APP_POSITION,
  APP_SOCIAL_POSITION,
  APP_PRIVILEGES,
  APP_AWARD,
  APP_MATERIAL_AID,
  APP_HOBBY,
  APP_TRAVEL,
  APP_WELLNESS,
  APP_TOUR,
  APP_ACTIVITIES,
  APP_CULTURAL_ACTIVITIES,
  APP_SUBDIVISIONS,
  APP_DEPARTMENRAL_HOUSING,
  APP_DORMIRTORY,
  APP_USERS,
  APP_DASHBOARD,
  APP_ACTION_LOG,
  APP_SEARCH
} from '../../constants/routes'

const drawerWidth = 280;

const useStyles = theme => ({
  appBar: {
    zIndex: theme.zIndex.drawer + 1,
    transition: theme.transitions.create(['width', 'margin'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
  },
  appBarShift: {
    marginLeft: drawerWidth,
    width: `calc(100% - ${drawerWidth}px)`,
    transition: theme.transitions.create(['width', 'margin'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  },
  menuButton: {
    marginRight: 36,
  },
  hide: {
    display: 'none',
  },
  drawer: {
    width: drawerWidth,
    flexShrink: 0,
    whiteSpace: 'nowrap',
  },
  drawerOpen: {
    width: drawerWidth,
    transition: theme.transitions.create('width', {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  },
  drawerClose: {
    transition: theme.transitions.create('width', {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
    overflowX: 'hidden',
    width: theme.spacing(7) + 1,
    [theme.breakpoints.up('sm')]: {
      width: theme.spacing(9) + 1,
    },
  },
  toolbar: {
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'flex-end',
    padding: theme.spacing(0, 1),
    ...theme.mixins.toolbar,
  },
  nested: {
    paddingLeft: theme.spacing(4),
  }
});

class Navigation extends React.Component {

  constructor(props) {
    super(props);

    this.state = {
      openPanel: false,
      openDirectories: false
    };

    this.handleDrawerOpen = this.handleDrawerOpen.bind(this);
    this.handleDrawerClose = this.handleDrawerClose.bind(this);
    this.handleClickDirectories = this.handleClickDirectories.bind(this);
    this.logout = this.logout.bind(this);
  }

  handleDrawerOpen() {
    this.setState({ openPanel: true });
  }

  handleDrawerClose() {
    this.setState({ openPanel: false, openDirectories: false });
  }

  handleClickDirectories() {
    if (this.state.openPanel) {
      this.setState({ openDirectories: !this.state.openDirectories });
    }
  }

  logout(event) {
    event.preventDefault();
    this.props.logout();
    this.props.history.push(AUTH);
  }

  render() {

    const { classes } = this.props;
    const { openPanel, openDirectories } = this.state;

    return (
      <div>
        {/* --- Header start --- */}
        <AppBar position="fixed" className={clsx(classes.appBar, { [classes.appBarShift]: openPanel })}>
          <Toolbar>
            <IconButton edge="start"
              color="inherit"
              aria-label="openPanel drawer"
              onClick={this.handleDrawerOpen}
              className={clsx(classes.menuButton, { [classes.hide]: openPanel })}>
              <MenuIcon />
            </IconButton>
            <Typography variant="h6" noWrap>
              TradeUnionCommittee.React.Web.GUI
          </Typography>
          </Toolbar>
        </AppBar>
        {/* --- Header end --- */}


        {/* --- Menu start --- */}
        <Drawer variant="permanent"
          className={clsx(classes.drawer, { [classes.drawerOpen]: openPanel, [classes.drawerClose]: !openPanel })}
          classes={{ paper: clsx({ [classes.drawerOpen]: openPanel, [classes.drawerClose]: !openPanel }) }}>
          <div className={classes.toolbar}>
            <IconButton onClick={this.handleDrawerClose}>
              <ChevronLeftIcon />
            </IconButton>
          </div>
          <Divider />

          <List>

            <ListItem button component={Link} to={APP_CREATE_EMPLOYEE} title="Create new employee">
              <ListItemIcon>
                <PersonAddIcon />
              </ListItemIcon>
              <ListItemText primary="Create new employee" />
            </ListItem>

            <ListItem button onClick={this.handleClickDirectories} title="Directories">
              <ListItemIcon>
                <CategoryIcon />
              </ListItemIcon>
              <ListItemText primary="Directories" />
              {openDirectories ? <ExpandLess /> : <ExpandMore />}
            </ListItem>

            <Collapse in={openDirectories} timeout="auto" unmountOnExit>
              <List component="div" disablePadding>

                <ListItem button className={classes.nested} component={Link} to={APP_POSITION}>
                  <ListItemIcon>
                    <BarChartIcon />
                  </ListItemIcon>
                  <ListItemText primary="Position" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to={APP_SOCIAL_POSITION}>
                  <ListItemIcon>
                    <AssessmentIcon />
                  </ListItemIcon>
                  <ListItemText primary="Social position" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to={APP_PRIVILEGES}>
                  <ListItemIcon>
                    <AccessibleIcon />
                  </ListItemIcon>
                  <ListItemText primary="Privileges" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to={APP_AWARD}>
                  <ListItemIcon>
                    <AttachMoneyIcon />
                  </ListItemIcon>
                  <ListItemText primary="Award" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to={APP_MATERIAL_AID}>
                  <ListItemIcon>
                    <AccessibilityIcon />
                  </ListItemIcon>
                  <ListItemText primary="Material Aid" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to={APP_HOBBY}>
                  <ListItemIcon>
                    <GolfCourseIcon />
                  </ListItemIcon>
                  <ListItemText primary="Hobby" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to={APP_TRAVEL}>
                  <ListItemIcon>
                    <TodayIcon />
                  </ListItemIcon>
                  <ListItemText primary="Travel" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to={APP_WELLNESS}>
                  <ListItemIcon>
                    <EventIcon />
                  </ListItemIcon>
                  <ListItemText primary="Wellness" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to={APP_TOUR}>
                  <ListItemIcon>
                    <EventAvailableIcon />
                  </ListItemIcon>
                  <ListItemText primary="Tour" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to={APP_ACTIVITIES}>
                  <ListItemIcon>
                    <EventNoteIcon />
                  </ListItemIcon>
                  <ListItemText primary="Activities" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to={APP_CULTURAL_ACTIVITIES}>
                  <ListItemIcon>
                    <DateRangeIcon />
                  </ListItemIcon>
                  <ListItemText primary="Cultural activities" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to={APP_SUBDIVISIONS}>
                  <ListItemIcon>
                    <SubdirectoryArrowRightIcon />
                  </ListItemIcon>
                  <ListItemText primary="Subdivisions" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to={APP_DEPARTMENRAL_HOUSING}>
                  <ListItemIcon>
                    <HomeWorkIcon />
                  </ListItemIcon>
                  <ListItemText primary="Departmental housing" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to={APP_DORMIRTORY}>
                  <ListItemIcon>
                    <HotelIcon />
                  </ListItemIcon>
                  <ListItemText primary="Dormitory" />
                </ListItem>

              </List>
            </Collapse>

            <ListItem button component={Link} to={APP_USERS} title="Users">
              <ListItemIcon>
                <PeopleIcon />
              </ListItemIcon>
              <ListItemText primary="Users" />
            </ListItem>

            <ListItem button component={Link} to={APP_DASHBOARD} title="Dashboard">
              <ListItemIcon>
                <DashboardIcon />
              </ListItemIcon>
              <ListItemText primary="Dashboard" />
            </ListItem>

            <ListItem button component={Link} to={APP_ACTION_LOG} title="Action log">
              <ListItemIcon>
                <StorageIcon />
              </ListItemIcon>
              <ListItemText primary="Action log" />
            </ListItem>

            <ListItem button component={Link} to={APP_SEARCH} title="Search">
              <ListItemIcon>
                <SearchIcon />
              </ListItemIcon>
              <ListItemText primary="Search" />
            </ListItem>

            <Divider />

            <ListItem button title="Logout" onClick={this.logout}>
              <ListItemIcon>
                <ExitToAppIcon />
              </ListItemIcon>
              <ListItemText primary="Logout" />
            </ListItem>

          </List>

        </Drawer>
        {/* --- Menu end --- */}
      </div>
    )
  }
}

export default withStyles(useStyles)(withRouter(Navigation))