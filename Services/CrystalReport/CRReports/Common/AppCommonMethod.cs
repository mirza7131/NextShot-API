using CRReports.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRReports.Common
{
    public static class AppCommonMethod
    {

        #region Null Or Empty Check Methods
        public static bool IsNullOrEmptyGuid(Guid? Id)
        {
            if (Id == null || Id == Guid.Empty)
                return true;
            else
                return false;

        }

        public static bool IsNullObject(object obj)
        {
            if (obj == null)
                return true;
            else
                return false;

        }

        public static bool IsNullorZerolong(long? id)
        {
            if (id.Equals(0) || id == 0 || id == null)
                return true;
            else
                return false;
        }
        public static bool IsNullorZeroInt(int? id)
        {
            if (id.Equals(0) || id == 0 || id == null)
                return true;
            else
                return false;
        }
        public static bool IsNullorEmptyDate(DateTime? date)
        {
            if (date == DateTime.MinValue || !date.HasValue)
                return true;
            else
                return false;
        }

        public static bool IsNullOrEmptyList<T>(List<T> list)
        {
            if (list == null || !list.Any())
                return true;
            else
                return false;
        }

        public static bool IsNullBool(bool? value)
        {
            if (value == null)
                return true;
            else
                return false;
        }

        #endregion


        #region Replace String Methods

        public static string RemoveDashes(string input)
        {
            return input.Replace("-", string.Empty);
        }

        // ADD DASHES IN CNIC

        public static string AddDashesInCnic(string input)
        {
            input.Replace("-", string.Empty);
            if (!input.Contains("-") && input.Length == 13)
            {
                input = input.Substring(0, 5) + "-" + input.Substring(5);
                input = input.Substring(0, 13) + "-" + input.Substring(13);
            }
            return input;
        }



        #endregion

        #region ReportTitle
        public static string ReportTittle(string input)
        {
            if(!string.IsNullOrEmpty(input))
            {
                if(input== "TokenIssued")
                {
                    return "Token Issued";
                }
                else if(input == "Registrations")
                {
                    return "Registration";
                }
                else if (input == "NewRegistrations")
                {
                    return "New Registrations";
                }
                else if (input == "ReVisits")
                {
                    return "Re-Visit";
                }  
                else if (input == "RefferForvitals")
                {
                    return "Reffered For Vitals";
                }
                else if (input == "VitalCollected")
                {
                    return "Vitals Collected";
                }
                //DOCTOR
                else if (input == "TotalRegistered")
                {
                    return "Reffered For Prescription";
                }
                else if (input == "Served")
                {
                    return "Patient Served";
                }
                else if (input == "NotServed")
                {
                    return "Patient Not Served";
                } 
                else if (input == "Prescribed")
                {
                    return "Medicine Prescribed";
                } 
                else if (input == "NotPerscribed")
                {
                    return "Medicine Not Prescribed";
                }  
                else if (input == "NotPerscribed")
                {
                    return "Medicine Not Prescribed";
                }
                else if (input == "InternalPatientCount")
                {
                    return "Internal Pharmacy";
                }
                else if (input == "ExternalPatientCount")
                {
                    return "External Pharmacy ";
                }
                else if (input == "IntExtPatientCount")
                {
                    return "Internal + External Pharmacy";
                } 
                else if (input == "IntExtPatientCount")
                {
                    return "Internal + External Pharmacy";
                } 
                else if (input == "TotalLabVisitCount")
                {
                    return "Tests Recommended";
                }
                else if (input == "TotalLabVisitCount")
                {
                    return "Tests Recommended";
                }
                else if (input == "InternalLabVisitCount")
                {
                    return "Facility Lab";
                }
                else if (input == "ExternalLabVisitCount")
                {
                    return "External Lab";
                }
                else if (input == "InternalExternalLabVisitCount")
                {
                    return "Internal + External Lab";
                }
                ///PHARMACY
                else if (input == "MedicineToBeIssued")
                {
                    return "Medicine To Be Issued";
                }
                else if (input == "MedicineIssued")
                {
                    return "Medicine Issued";
                }
                else if (input == "MedicineNotIssued")
                {
                    return "Medicine Not Issued";
                }
                else if (input == "MedicineNotIssued")
                {
                    return "Medicine Not Issued";
                } 

                ///PATHOLGY
                else if (input == "TotalLabTestCount")
                {
                    return "Tests Recommended";
                } 
                else if (input == "TotalLabTestCount")
                {
                    return "Tests Recommended";
                } 
                else if (input == "InternalLabTestCount")
                {
                    return "Facility Lab";
                }
                else if (input == "ExternalLabTestCount")
                {
                    return "External Labs";
                } 
                
                else if (input == "InternalExternalLabTestCount")
                {
                    return "Internal + External LAB";
                }
                // Lab Test Status
                else if (input == "SampleTobeCollected")
                {
                    return "Sample To be Collected";
                }
                else if (input == "SampleCollected")
                {
                    return "Sample Collected";
                }
                else if (input == "SampleRejected")
                {
                    return "Sample Rejected";
                }
                else if (input == "SamplePendingTobeCollected")
                {
                    return "Sample Not Collected ";
                }
                else if (input == "ReportGenerated")
                {
                    return "Report Generated";
                }
                else if (input == "PendingReports")
                {
                    return "Pending Reports";
                }

                ///Pending sample
                else if (input == "PendingSampleInThreeDays")
                {
                    return "Pending Sample With In 3 Days";
                }
                else if (input == "PendingSampleInSevenDays")
                {
                    return "Pending Sample With In 7 Days";
                }
                else if (input == "PendingSampleIn15Days")
                {
                    return "Pending Sample With In 15 Days";
                }
                else if (input == "PendingSampleIn30Days")
                {
                    return "Pending Sample With In 30 Days";
                }
                else if (input == "PendingSampleGreater30Days")
                {
                    return "Pending Sample more then 30 days";
                }
                ///Pending Report
                else if (input == "PendingReportInThreeDays")
                {
                    return "Pending Report With In 3 Days";
                }
                else if (input == "PendingReportInSevenDays")
                {
                    return "Pending Report With In 7 Days";
                }
                else if (input == "PendingReportIn15Days")
                {
                    return "Pending Report With In 15 Days";
                }
                else if (input == "PendingReportIn30Days")
                {
                    return "Pending Report With In 30 Days";
                }
                else if (input == "PendingReportGreater30Days")
                {
                    return "Pending Report more then 30 days";
                }
                ///UHI Dashboard
                else if (input == "TotalClaims")
                {
                    return "Total Claims";
                }
                else if (input == "ClaimSubmitted")
                {
                    return "Submitted For Verification";
                }
                else if (input == "Eligible")
                {
                    return "Pending To Be Submitted";
                }
                else if (input == "ClaimApproved")
                {
                    return "Verified Claims ";
                }
                else if (input == "ClaimSubmitted")
                {
                    return "Pending To Be Verified";
                }
                else if (input == "ClaimReSubmitted")
                {
                    return "Returned For Re-Submission";
                }
                else if (input == "ClaimRejected")
                {
                    return "Rejected Claims";
                }
                else if (input == "NotEligible")
                {
                    return "Not Eligible For SSC";
                } 
                else if (input == "UnAttended")
                {
                    return "UnAttended";
                }

                //DrugAddict
                else if (input == "DrugAddict")
                {
                    return "Drug Addicts";
                }
                else if (input == "Known")
                {
                    return "Identified";
                }
                else if (input == "Known")
                {
                    return "Identified";
                }
                else if (input == "UnKnown")
                {
                    return "Un-Identified";
                }
                else if (input == "Verified")
                {
                    return "Verified by NADRA";
                }
                else if (input == "UnVerified")
                {
                    return "Pending Verification by NADRA";
                }
                else if (input == "HepatitisB")
                {
                    return "Hepatitis B";
                }
                else if (input == "HepatitisC")
                {
                    return "Hepatitis C";
                }
                else if (input == "HIV")
                {
                    return "HIV";
                } 
                else if (input == "CoInfected")
                {
                    return "CO-Infected";
                } 
                else if (input == "RefferedToSW")
                {
                    return "Reffered To Social Welfare";
                } 
                else if (input == "Admitted")
                {
                    return "Admitted";
                } 
                else if (input == "Discharged")
                {
                    return "Discharged";
                }
                ///hcp
                else if (input == "PCRforHCVRNA")
                {
                    return "HCV RNA Recommended";
                }
                else if (input == "PCRforHBVDNA")
                {
                    return "HBV DNA Recommended";
                } else if (input == "PCRforHCVRNA")
                {
                    return "HCV RNA Recommended";
                } 
                else if (input == "PCRforHCVRNA")
                {
                    return "HCV RNA Recommended";
                } 
                else if (input == "PCRforHBVDNAPositive")
                {
                    return "HBV DNA Positive";
                }
                else if (input == "PCRforHBVDNANegative")
                {
                    return "HBV DNA Negative";
                }
                else if (input == "PCRforHCVRNANegative")
                {
                    return "HCV RNA Negative";
                }
                //paraplegic
                else if (input == "TotalRegisteredSurgeryForm")
                {
                    return "Total Registered For Surgery";
                }
                else if (input == "VitalCollectedSurgeryForm")
                {
                    return "Vital Collected For Surgery";
                } 
                else if (input == "ServedSurgeryForm")
                {
                    return "Surgery Patient Served ";
                } 
                else if (input == "NotServedSurgeryForm")
                {
                    return "Surgery Patient Not Served ";
                }

                //paraplegic Physiotherapy
                else if (input == "TotalRegisteredPhysiotherapyFormOPD")
                {
                    return "Total Registered For Physiotherapy";
                }
                else if (input == "VitalCollectedPhysiotherapyFormOPD")
                {
                    return "Vital Collected For Physiotherapy";
                }
                else if (input == "ServedPhysiotherapyFormOPD")
                {
                    return "Physiotherapy Patient Served ";
                }
                else if (input == "NotServedPhysiotherapyFormOPD")
                {
                    return "Physiotherapy Patient Not Served ";
                } 
                //paraplegic Psychiatry
                else if (input == "TotalRegisteredPsychiatryForm")
                {
                    return "Total Registered For Psychiatry";
                }
                else if (input == "VitalCollectedPsychiatryForm")
                {
                    return "Vital Collected For Psychiatry";
                }
                else if (input == "ServedPsychiatryForm")
                {
                    return "Psychiatry Patient Served ";
                }
                else if (input == "NotServedPsychiatryForm")
                {
                    return "Psychiatry Patient Not Served ";
                }
                //paraplegic Nutrition
                else if (input == "TotalRegisteredNutritionForm")
                {
                    return "Total Registered For Nutrition";
                }
                else if (input == "VitalCollectedNutritionForm")
                {
                    return "Vital Collected For Nutrition";
                }
                else if (input == "ServedNutritionForm")
                {
                    return "Nutrition Patient Served ";
                }
                else if (input == "NotServedNutritionForm")
                {
                    return "Nutrition Patient Not Served ";
                }
                //paraplegic Speech Therapy
                else if (input == "TotalRegisteredSpeechTherapyForm")
                {
                    return "Total Registered For Speech Therapy";
                }
                else if (input == "VitalCollectedSpeechTherapyForm")
                {
                    return "Vital Collected For Speech Therapy";
                }
                else if (input == "ServedSpeechTherapyForm")
                {
                    return "Speech Therapy Patient Served ";
                }
                else if (input == "NotServedSpeechTherapyForm")
                {
                    return "Speech Therapy Patient Not Served ";
                }
                //paraplegic Psychology
                else if (input == "TotalRegisteredPsychologyForm")
                {
                    return "Total Registered For Psychology";
                }
                else if (input == "VitalCollectedPsychologyForm")
                {
                    return "Vital Collected For Psychology";
                }
                else if (input == "ServedPsychologyForm")
                {
                    return "Psychology Patient Served ";
                }
                else if (input == "NotServedPsychologyForm")
                {
                    return "Psychology Patient Not Served ";
                }
                //paraplegic Occupational Therapy
                else if (input == "TotalRegisteredOccupationalTherapyForm")
                {
                    return "Total Registered For Occupational Therapy";
                }
                else if (input == "VitalCollectedOccupationalTherapyForm")
                {
                    return "Vital Collected For Occupational Therapy";
                }
                else if (input == "ServedOccupationalTherapyForm")
                {
                    return "Occupational Therapy Patient Served ";
                }
                else if (input == "NotServedOccupationalTherapyForm")
                {
                    return "Occupational Therapy Patient Not Served ";
                }
                //IPD
                else if (input == "ActiveAdmissionsPatients")
                {
                    return "Active Admissions";
                }
                else if (input == "VitalCollectedPatients")
                {
                    return "Vital Collected Patients";
                }
                else if (input == "ServedPatients")
                {
                    return "Served Patients";
                }
                else if (input == "PrescribedPatients")
                {
                    return "Prescribed Patients";
                }
                else if (input == "InternalPrescribedPatients")
                {
                    return "Internal Prescribed Patients";
                }
                else if (input == "ExternalPrescribedPatients")
                {
                    return "External Prescribed Patients";
                }
                else if (input == "InternalExternalPrescribedPatients")
                {
                    return "External-Internal Prescribed Patients";
                }
                else if (input == "TotalLabTestRecommended")
                {
                    return "Lab Test Recommended";
                }
                else if (input == "InternalTestRecommended")
                {
                    return "Facility Lab Test";
                }
                else if (input == "ExternalTestRecommended")
                {
                    return "External Lab Test";
                }
                else if (input == "InternalExternalTestRecommended")
                {
                    return "Internal-External Lab Test";
                }else if (input == "OPDDispenseReport")
                {
                    return "Medicine Dispense Report";
                }
                else if (input == "OPDStockReport")
                {
                    return "OPD Stock Report";
                }
                else if (input == "NeckHolding0to5")
                {
                    return "Neck Holding 0 to 5 Month";
                }
                else if (input == "NeckHolding6to10")
                {
                    return "Neck Holding 6 to 10 Month";
                }
                else if (input == "NeckHolding11to15")
                {
                    return "Neck Holding 11 to 15 Month";
                }
                else if (input == "NeckHoldingGreater15")
                {
                    return "Neck Holding Greater Than 15 Month";
                }

                else if (input == "Sitting0to5")
                {
                    return "Sitting 0 to 5 Month";
                }
                else if (input == "Sitting6to10")
                {
                    return "Sitting 6 to 10 Month";
                }
                else if (input == "Sitting11to15")
                {
                    return "Sitting 11 to 15 Month";
                }
                else if (input == "SittingGreater15")
                {
                    return "Sitting Greater Than 15 Month";
                }

                else if (input == "Walking0to5")
                {
                    return "Walking 0 to 5 Month";
                }
                else if (input == "Walking6to10")
                {
                    return "Walking 6 to 10 Month";
                }
                else if (input == "Walking11to15")
                {
                    return "Walking 11 to 15 Month";
                }
                else if (input == "WalkingGreater15")
                {
                    return "Walking Greater Than 15 Month";
                }

                else if (input == "Standing0to5")
                {
                    return "Standing 0 to 5 Month";
                }
                else if (input == "Standing6to10")
                {
                    return "Standing 6 to 10 Month";
                }
                else if (input == "Standing11to15")
                {
                    return "Standing 11 to 15 Month";
                }
                else if (input == "StandingGreater15")
                {
                    return "Standing Greater Than 15 Month";
                }

                else if (input == "Cooing0to5")
                {
                    return "Cooing 0 to 5 Month";
                }
                else if (input == "Cooing6to10")
                {
                    return "Cooing 6 to 10 Month";
                }
                else if (input == "Cooing11to15")
                {
                    return "Cooing 11 to 15 Month";
                }
                else if (input == "CooingGreater15")
                {
                    return "Cooing Greater Than 15 Month";
                }

                else if (input == "Babbling0to5")
                {
                    return "Babbling 0 to 5 Month";
                }
                else if (input == "Babbling6to10")
                {
                    return "Babbling 6 to 10 Month";
                }
                else if (input == "Babbling11to15")
                {
                    return "Babbling 11 to 15 Month";
                }
                else if (input == "BabblingGreater15")
                {
                    return "Babbling Greater Than 15 Month";
                }

                else if (input == "SingleWord0to5")
                {
                    return "Single Word 0 to 5 Month";
                }
                else if (input == "SingleWord6to10")
                {
                    return "Single Word 6 to 10 Month";
                }
                else if (input == "SingleWord11to15")
                {
                    return "Single Word 11 to 15 Month";
                }
                else if (input == "SingleWordGreater15")
                {
                    return "Single Word Greater Than 15 Month";
                }


                else if (input == "SpeechLevel0to5")
                {
                    return "Speech Level 0 to 5 Month";
                }
                else if (input == "SpeechLevel6to10")
                {
                    return "Speech Level 6 to 10 Month";
                }
                else if (input == "SpeechLevel11to15")
                {
                    return "Speech Level 11 to 15 Month";
                }
                else if (input == "SpeechLevelGreater15")
                {
                    return "Speech Level Greater Than 15 Month";
                }

                else if (input == "NoSpeechMilestone")
                {
                    return "Patients With No Speech Milestone";
                }
                else if (input == "SpeechMilestone")
                {
                    return "Patients With Speech Milestone";
                }

                else if (input == "NoFamilyHistory")
                {
                    return "Patients With No Family History";
                }
                else if (input == "FamilyHistory")
                {
                    return "Patients With Family History";
                }

                else if (input == "NoHearingLoss")
                {
                    return "Patients With No Hearing Loss";
                }
                else if (input == "HearingLoss")
                {
                    return "Patients With Hearing Loss";
                }


                else if (input == "NoDevelopmentMileStone")
                {
                    return "Patients With No Development Milestone";
                }
                else if (input == "DevelopmentMileStone")
                {
                    return "Patients With Development Milestone";
                }
                else if (input == "ArticulationSounderrors")
                {
                    return "Coversation Speech Sound Errors";
                }

                else if (input == "Articulationintelligibility")
                {
                    return "Coversation Speech intelligibility";
                }

                else if (input == "ArticulationOthers")
                {
                    return "Others Coversation Speech";
                }

                else if (input == "dysfluencyRepetitions")
                {
                    return "Repetitions dysfluency";
                }

                else if (input == "dysfluencyProlongation")
                {
                    return "Prolongation dysfluency";
                }

                else if (input == "dysfluencySilentpause")
                {
                    return "Silent pause dysfluency";
                }
                else if (input == "dysfluencyOthers")
                {
                    return "Other dysfluency";
                }
                else if (input == "VoiceHoarse")
                {
                    return "Hoarse Voice Quality";
                }

                else if (input == "VoiceAphonic")
                {
                    return "Aphonic Voice Quality";
                }
                
                else if (input == "VoiceAphonic")
                {
                    return "Others Voice Quality";
                }

                else if (input == "PitchToohigh")
                {
                    return "Pitch Too high";
                }

                else if (input == "PitchToolow")
                {
                    return "Pitch Too low";
                }
                else if (input == "PitchOthers")
                {
                    return "Other Pitch";
                }

                else if (input == "ResonanceNasal")
                {
                    return "Nasal Resonance";
                }
                else if (input == "ResonanceDenasal")
                {
                    return "Denasal Resonance";
                }
                else if (input == "ResonanceMixed")
                {
                    return "Mixed Resonance";
                }
                else if (input == "PlanOfCareSelfTalk")
                {
                    return "Self-Talk Plan Of Care";
                }
                else if (input == "PlanOfCareParallel")
                {
                    return "Parallel talk Plan Of Care";
                }
                else if (input == "PlanOfCareFocused")
                {
                    return "Focused Stimulation Plan Of Care";
                }
                else if (input == "PlanOfCareOthers")
                {
                    return "Other Plan Of Care";
                }
                else if (input == "ScheduleWeekly")
                {
                    return "Weekly Plan Of Care Schedule";
                }
                else if (input == "ScheduleFortnightly")
                {
                    return "Fortnightly Plan Of Care Schedule";
                }
                else if (input == "ScheduleMonthly")
                {
                    return "Monthly Plan Of Care Schedule";
                }
                else if (input == "ScheduleOthers")
                {
                    return "Other Plan Of Care Schedule";
                }
                else if (input == "PastHistory")
                {
                    return "Patients With Past History";
                }
                else if (input == "NoPastHistory")
                {
                    return "Patients With No Past History";
                }
                else if (input == "PsychologicalTestApplied")
                {
                    return "Patients With Psychological Test Applied";
                }
                else if (input == "NoPsychologicalTestApplied")
                {
                    return "Patients With No Psychological Test Applied";
                }
                else if (input == "EstimatedIntellectualFunctioning")
                {
                    return "Estimated Intellectual Functioning";
                }
                else if (input == "CognitiveDeficits")
                {
                    return "Cognitive Deficits";
                }
                else if (input == "PerceptualProcess")
                {
                    return "Perceptual Process";
                }
                else if (input == "ThroughContent")
                {
                    return "Through Content";
                }
                else if (input == "ThroughProcess")
                {
                    return "Through Process";
                }
                else if (input == "ExaminationFindings")
                {
                    return "Patients With Examination Findings";
                }
                else if (input == "NoExaminationFindings")
                {
                    return "Patients With No Examination Findings";
                }
                else if (input == "Comorbidity")
                {
                    return "Patients With Comorbidity";
                }
                else if (input == "NoComorbidity")
                {
                    return "Patients With No Comorbidity";
                }
                else if(input == "DoctorWiseCount")
                {
                   return "Patient Report By Doctor";
                }


            }


            return input;
        }
        #endregion

    }
}