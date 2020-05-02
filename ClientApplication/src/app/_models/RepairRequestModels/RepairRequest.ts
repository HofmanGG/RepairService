export interface RepairRequest {
  id: number;

  productName: string;
  comment: string;

  requestDay: number;
  requestMonth: number;
  requestYear: number;

  repairStatus: string;

  userInfoId: number;
  email: number;
}
