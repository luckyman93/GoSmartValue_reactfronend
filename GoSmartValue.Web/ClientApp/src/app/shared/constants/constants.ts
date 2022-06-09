import {PropertyFeature} from "../models/property-feature";
import {IPropertyDetail} from "../models/interfaces/property-detail";
import {PropertyDetails} from "../models/property-details";


export const developmentStage: string[] = ['Undeveloped', 'Developed', 'Partially Developed'];

export const featureType: string[] = [
  'Garage',
  'SwimmingPool',
  'WireFenced',
  'ElectricFence',
  'FirePlace',
  'BoundaryWall',
  'MotorizedGate',
  'OutdoorEntertainment',
  'Paved',
  'Other',
];

export const roomType: string[] = [
  'BedRoom', //0
  'SittingRoom', //1
  'BathRoom', //2
  'Kitchen', //3
  'LaundryRoom', //4
  'Toilets', //5
  'Pantry', //6
  'Garages', //7
];

export const features: PropertyFeature[] = [
  new PropertyFeature('Garage', 0, false),
  new PropertyFeature('SwimmingPool', 0, false),
  new PropertyFeature('WireFenced', 0, false),
  new PropertyFeature('ElectricFence', 0, false),
  new PropertyFeature('FirePlace', 0, false),
  new PropertyFeature('BoundaryWall', 0, false),
  new PropertyFeature('MotorizedGate', 0, false),
  new PropertyFeature('OutdoorEntertainment', 0, false),
  new PropertyFeature('Paved', 0, false),
];

export const propertyDetail: IPropertyDetail[] = [
  new PropertyDetails(roomType[0], 0, 1),
  new PropertyDetails(roomType[1], 0, 2),
  new PropertyDetails(roomType[2], 0, 3),
  new PropertyDetails(roomType[3], 0, 4),
  new PropertyDetails(roomType[4], 0, 5),
  new PropertyDetails(roomType[5], 0, 6),
  new PropertyDetails(roomType[7], 0, 7),
];
