import React from 'react';
import { Link } from "react-router-dom";
import clsx from 'clsx';
import { makeStyles } from '@material-ui/core/styles';
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

const drawerWidth = 280;

const useStyles = makeStyles(theme => ({
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
    backgroundColor: '#ffffff' , 
    backgroundImage: 'linear-gradient(315deg, #ffffff 0%, #d7e1ec 74%)',
    width: drawerWidth,
    transition: theme.transitions.create('width', {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  },
  drawerClose: {
    backgroundColor: '#ffffff' , 
    backgroundImage: 'linear-gradient(315deg, #ffffff 0%, #d7e1ec 74%)',
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
}));

export default function Navigation() {
  const classes = useStyles();
  const [openPanel, setOpenPanel] = React.useState(false);
  const [openDirectories, setOpenDirectories] = React.useState(false);

  const handleDrawerOpen = () => {
    setOpenPanel(true);
  };

  const handleDrawerClose = () => {
    setOpenPanel(false);
    setOpenDirectories(false);
  };

  const handleClickDirectories = () => {
    if (openPanel) {
      setOpenDirectories(!openDirectories);
    }
  };

  return (
    <div>
      {/* --- Header start --- */}
      <AppBar position="fixed" className={clsx(classes.appBar, { [classes.appBarShift]: openPanel })}>
        <Toolbar>
          <IconButton edge="start"
            color="inherit"
            aria-label="openPanel drawer"
            onClick={handleDrawerOpen}
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
          <IconButton onClick={handleDrawerClose}>
            <ChevronLeftIcon />
          </IconButton>
        </div>
        <Divider />

        <List>

          <ListItem button component={Link} to="/create-employee" title="Create new employee">
            <ListItemIcon>
              <PersonAddIcon />
            </ListItemIcon>
            <ListItemText primary="Create new employee" />
          </ListItem>

          <ListItem button onClick={handleClickDirectories} title="Directories">
            <ListItemIcon>
              <CategoryIcon />
            </ListItemIcon>
            <ListItemText primary="Directories" />
            {openDirectories ? <ExpandLess /> : <ExpandMore />}
          </ListItem>

          <Collapse in={openDirectories} timeout="auto" unmountOnExit>
            <List component="div" disablePadding>

              <ListItem button className={classes.nested} component={Link} to="/position">
                <ListItemIcon>
                  <BarChartIcon />
                </ListItemIcon>
                <ListItemText primary="Position" />
              </ListItem>

              <ListItem button className={classes.nested} component={Link} to="/social-position">
                <ListItemIcon>
                  <AssessmentIcon />
                </ListItemIcon>
                <ListItemText primary="Social position" />
              </ListItem>

              <ListItem button className={classes.nested} component={Link} to="/privileges">
                <ListItemIcon>
                  <AccessibleIcon />
                </ListItemIcon>
                <ListItemText primary="Privileges" />
              </ListItem>

              <ListItem button className={classes.nested} component={Link} to="/award">
                <ListItemIcon>
                  <AttachMoneyIcon />
                </ListItemIcon>
                <ListItemText primary="Award" />
              </ListItem>

              <ListItem button className={classes.nested} component={Link} to="/material-aid">
                <ListItemIcon>
                  <AccessibilityIcon />
                </ListItemIcon>
                <ListItemText primary="Material Aid" />
              </ListItem>

              <ListItem button className={classes.nested} component={Link} to="/hobby">
                <ListItemIcon>
                  <GolfCourseIcon />
                </ListItemIcon>
                <ListItemText primary="Hobby" />
              </ListItem>

              <ListItem button className={classes.nested} component={Link} to="/travel">
                <ListItemIcon>
                  <TodayIcon />
                </ListItemIcon>
                <ListItemText primary="Travel" />
              </ListItem>

              <ListItem button className={classes.nested} component={Link} to="/wellness">
                <ListItemIcon>
                  <EventIcon />
                </ListItemIcon>
                <ListItemText primary="Wellness" />
              </ListItem>

              <ListItem button className={classes.nested} component={Link} to="/tour">
                <ListItemIcon>
                  <EventAvailableIcon />
                </ListItemIcon>
                <ListItemText primary="Tour" />
              </ListItem>

              <ListItem button className={classes.nested} component={Link} to="/activities">
                <ListItemIcon>
                  <EventNoteIcon />
                </ListItemIcon>
                <ListItemText primary="Activities" />
              </ListItem>

              <ListItem button className={classes.nested} component={Link} to="/cultural-activities">
                <ListItemIcon>
                  <DateRangeIcon />
                </ListItemIcon>
                <ListItemText primary="Cultural activities" />
              </ListItem>

              <ListItem button className={classes.nested} component={Link} to="/subdivisions">
                <ListItemIcon>
                  <SubdirectoryArrowRightIcon />
                </ListItemIcon>
                <ListItemText primary="Subdivisions" />
              </ListItem>

              <ListItem button className={classes.nested} component={Link} to="/departmental-housing">
                <ListItemIcon>
                  <HomeWorkIcon />
                </ListItemIcon>
                <ListItemText primary="Departmental housing" />
              </ListItem>

              <ListItem button className={classes.nested} component={Link} to="/dormitory">
                <ListItemIcon>
                  <HotelIcon />
                </ListItemIcon>
                <ListItemText primary="Dormitory" />
              </ListItem>

            </List>
          </Collapse>

          <ListItem button component={Link} to="/users" title="Users">
            <ListItemIcon>
              <PeopleIcon />
            </ListItemIcon>
            <ListItemText primary="Users" />
          </ListItem>

          <ListItem button component={Link} to="/dashboard" title="Dashboard">
            <ListItemIcon>
              <DashboardIcon />
            </ListItemIcon>
            <ListItemText primary="Dashboard" />
          </ListItem>

          <ListItem button component={Link} to="/action-log" title="Action log">
            <ListItemIcon>
              <StorageIcon />
            </ListItemIcon>
            <ListItemText primary="Action log" />
          </ListItem>

          <ListItem button component={Link} to="/search" title="Search">
            <ListItemIcon>
              <SearchIcon />
            </ListItemIcon>
            <ListItemText primary="Search" />
          </ListItem>

          <Divider />

          <ListItem button component={Link} to="/logout" title="Logout">
            <ListItemIcon>
              <ExitToAppIcon />
            </ListItemIcon>
            <ListItemText primary="Logout" />
          </ListItem>

        </List>

      </Drawer>
      {/* --- Menu end --- */}
    </div>
  );
}
